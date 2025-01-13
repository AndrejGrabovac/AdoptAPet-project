using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdoptAPet.Models;
using Microsoft.IdentityModel.Tokens;

namespace AdoptAPet.Services;

public class TokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string IssueToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name)
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken()
    {
        return new RefreshToken()
        {
            Value = GenerateRefreshTokenValue(),
            ExpiresAt = DateTime.Now.AddDays(30)
        };
    }

    private string GenerateRefreshTokenValue()
    {
        var randomNumber = new byte[32];  // Prepare a buffer to hold the random bytes.
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);  // Fill the buffer with cryptographically strong random bytes.
            return Convert.ToBase64String(randomNumber);  // Convert the bytes to a Base64 string and return.
        }
    }
}


