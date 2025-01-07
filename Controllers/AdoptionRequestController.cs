
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdoptAPet.Data;
using AdoptAPet.DTOs.AdoptionRequest;
using AdoptAPet.Mappers;
using AdoptAPet.Models;

namespace AdoptAPet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionRequestController : ControllerBase
    {
        private readonly DataContext _context;

        public AdoptionRequestController(DataContext context)
        {
            _context = context;
        }

        // GET: api/AdoptionRequest
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdoptionRequest>>> GetAdoptionRequests(int page = 1, int pageSize = 5)
        {
            var adoptionRequests = await _context.AdoptionRequests
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(adoptionRequests.Select(adoptionRequest => adoptionRequest.ToAdoptionRequestDto()).ToList());
        }

        // GET: api/AdoptionRequest/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdoptionRequestDetailDto>> GetAdoptionRequest(int id)
        {
            var adoptionRequest = await _context.AdoptionRequests.FindAsync(id);

            if (adoptionRequest == null)
            {
                return NotFound();
            }

            return Ok(adoptionRequest.ToAdoptionRequestDetailDto());
        }

        // PUT: api/AdoptionRequest/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdoptionRequest(int id, AdoptionRequestUpdateDto adoptionRequestDto)
        {
            if (id != adoptionRequestDto.Id)
            {
                return BadRequest();
            }

            AdoptionRequest? adoptionRequest = await _context.AdoptionRequests.FindAsync(id);

            if (adoptionRequest == null)
            {
                return NotFound();
            }
            
            _context.Entry(adoptionRequest).CurrentValues.SetValues(adoptionRequestDto);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                if (!AdoptionRequestExists(id))
                {
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/AdoptionRequest
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AdoptionRequest>> PostAdoptionRequest(AdoptionRequestCreateDto adoptionRequestDto)
        {
            AdoptionRequest adoptionRequest = adoptionRequestDto.ToAdoptionRequest();
            _context.AdoptionRequests.Add(adoptionRequest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdoptionRequest", new { id = adoptionRequest.Id }, adoptionRequest);
        }

        // DELETE: api/AdoptionRequest/5
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
