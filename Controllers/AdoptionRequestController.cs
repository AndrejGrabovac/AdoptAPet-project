
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdoptAPet.Data;
using AdoptAPet.DTOs.AdoptionRequest;
using AdoptAPet.Mappers;
using AdoptAPet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

namespace AdoptAPet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionRequestController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<AdoptionRequestController> _logger;

        public AdoptionRequestController(DataContext context, ILogger<AdoptionRequestController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/AdoptionRequest
        //[Authorize(Roles = "Admin,User")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdoptionRequest>>> GetAdoptionRequests(int page = 1, int pageSize = 5)
        {
            _logger.LogInformation("Fetching adoption requests for page {Page} with page size {PageSize}", page, pageSize);
            var adoptionRequests = await _context.AdoptionRequests
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .OrderBy(ar => ar.Date)
                .ToListAsync();

            return Ok(adoptionRequests.Select(adoptionRequest => adoptionRequest.ToAdoptionRequestDto()).ToList());
        }

        // GET: api/AdoptionRequest/5
        [Authorize(Roles = "Admin,User")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AdoptionRequestDetailDto>> GetAdoptionRequest(int id)
        {
            var adoptionRequest = await _context.AdoptionRequests
                .OrderBy(ar => ar.Date)
                .FirstOrDefaultAsync(ar => ar.Id == id);
            
            if (adoptionRequest == null)
            {
                return NotFound();
            }

            return Ok(adoptionRequest.ToAdoptionRequestDetailDto());
        }

        // PUT: api/AdoptionRequest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdoptionRequest(int id, AdoptionRequestUpdateDto adoptionRequestDto)
        {
            if (id != adoptionRequestDto.Id)
            {
                return BadRequest();
            }

            AdoptionRequest? adoptionRequest = _context.AdoptionRequests
                .Include(ar => ar.Pet)
                .Include(ar => ar.User)
                .FirstOrDefault(ar => ar.Id == id);

            if (adoptionRequest == null)
            {
                return NotFound($"Adoption request with id: {id} not found.");
            }

            Pet? pet = _context.Pets
                .Include(p => p.AdoptionRequests)
                .FirstOrDefault(p => p.Id == adoptionRequest.PetId);
            if (pet == null)
            {
                return NotFound("Pet not found");
            }

            // Check if the pet is already adopted by another user
            if (adoptionRequestDto.IsApproved && pet.IsAdopted && pet.AdoptionRequests
                    .Any(ar => ar.IsApproved && ar.UserId != adoptionRequest.UserId))
            {
                return BadRequest("This pet is already adopted by another user.");
            }

            bool wasApproved = adoptionRequest.IsApproved;
            _context.Entry(adoptionRequest).CurrentValues.SetValues(adoptionRequestDto);

            if (!wasApproved && adoptionRequestDto.IsApproved)
            {
                pet.IsAdopted = true;
                _context.Entry(pet).State = EntityState.Modified;
            }
            else if (wasApproved && !adoptionRequestDto.IsApproved)
            {
                if (pet.AdoptionRequests.Any(ar => ar.IsApproved && ar.Id != adoptionRequest.Id))
                {
                    return BadRequest("Cannot unapprove this request as the pet is adopted by another user.");
                }
                pet.IsAdopted = false;
                _context.Entry(pet).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!AdoptionRequestExists(id))
                {
                    return NotFound($"Adoption request with id {id} not found.");
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/AdoptionRequest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "Admin,User")]
        [HttpPost]
        public async Task<ActionResult<AdoptionRequest>> PostAdoptionRequest(AdoptionRequestCreateDto adoptionRequestDto)
        {   
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User Id not found in JWT token.");
            }

            int authenticatedUserId = int.Parse(userId);

            bool requestExists = await _context.AdoptionRequests
                .AnyAsync(ar => ar.UserId == authenticatedUserId && ar.PetId == adoptionRequestDto.PetId);

            if (requestExists)
            {
                return BadRequest("You have already sent an adoption request for this pet.");
            }
            
            Pet? pet = await _context.Pets.FindAsync(adoptionRequestDto.PetId);
            if (pet == null)
            {
                return NotFound("Pet not found.");
            }

            if (pet.IsAdopted)
            {
                return BadRequest("This pet is already adopted!");
            }
            
            AdoptionRequest adoptionRequest = adoptionRequestDto.ToAdoptionRequest();
            adoptionRequest.UserId = authenticatedUserId;
            _context.AdoptionRequests.Add(adoptionRequest);
            
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdoptionRequest", new { id = adoptionRequest.Id }, adoptionRequest);
        }

        // DELETE: api/AdoptionRequest/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdoptionRequest(int id)
        {
            var adoptionRequest = await _context.AdoptionRequests.FindAsync(id);
            if (adoptionRequest == null)
            {
                return NotFound();
            }

            _context.AdoptionRequests.Remove(adoptionRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdoptionRequestExists(int id)
        {
            return _context.AdoptionRequests.Any(e => e.Id == id);
        }
    }
}
