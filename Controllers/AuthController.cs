using System.Security.Claims;
using AdoptAPet.Data;
using AdoptAPet.DTOs.Login;
using AdoptAPet.DTOs.Register;
using AdoptAPet.Models;
using AdoptAPet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdoptAPet.Controllers;

[Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenService _tokenService;
        private readonly DataContext _context;

        public AuthController(TokenService tokenService, DataContext context)
        {
            _tokenService = tokenService;
            _context = context;

        }
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (await _context.Users.AnyAsync(u => u.Email == registerDto.Email))
            {
                return BadRequest("Email is already taken.");
            }

            CreatePasswordHash(registerDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User user = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                RoleId = 2 //Default value of newly created user is User instead of Admin
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok("User registered successfully.");
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginDto loginDto)
        {
            User? user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email);

            if (user == null || !VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid user credentials.");
            }

            string token = _tokenService.IssueToken(user);
            RefreshToken refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(new TokenResponse()
            {
                AccessToken = token,
                RefreshToken = refreshToken.Value
            });
        }
        private bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }
            return true;
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<TokenResponse>> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            User? user = await _context.Users
                .Include(u => u.RefreshTokens)
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u =>
                    u.RefreshTokens.Any(rt =>
                        rt.Value == refreshTokenRequest.RefreshToken
                        && rt.ExpiresAt > DateTime.Now));

            RefreshToken? existingRefreshToken = await _context.RefreshToken
                .Where(rt => rt.Value == refreshTokenRequest.RefreshToken)
                .FirstOrDefaultAsync();
                
            if (user == null || existingRefreshToken == null)
            {
                return Unauthorized("Invalid refresh token.");
            }

            _context.RefreshToken.Remove(existingRefreshToken);
            //_context.RefreshTokens.RemoveRange(user.RefreshTokens.Where(rt => rt.Value == refreshTokenRequest.RefreshToken));
            
            string token = _tokenService.IssueToken(user);
            RefreshToken refreshToken = _tokenService.GenerateRefreshToken();
            
            user.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(new TokenResponse()
            {
                AccessToken = token,
                RefreshToken = refreshToken.Value
            });
        }


        [HttpPost("logout")]
        [Authorize]
        public async Task<ActionResult<TokenResponse>> Logout()
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User Id not found in JWT token.");
            }

            int authenticatedUserId = int.Parse(userId);

            User? user = await _context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Id == authenticatedUserId);
            
            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            if (user.RefreshTokens.Count == 0)
            {
                return Unauthorized("You are already logged out.");
            }
            
            user.RefreshTokens = new List<RefreshToken>();
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }