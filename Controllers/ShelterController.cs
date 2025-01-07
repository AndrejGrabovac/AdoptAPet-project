
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdoptAPet.Data;
using AdoptAPet.DTOs.Shelter;
using AdoptAPet.Mappers;
using AdoptAPet.Models;

namespace AdoptAPet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShelterController : ControllerBase
    {
        private readonly DataContext _context;

        public ShelterController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Shelter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShelterDto>>> GetShelters(int page = 1, int pageSize = 5)
        {   
            var shelters = await _context.Shelters
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(shelters.Select(shelter => shelter.ToShelterDto()).ToList());
            
            /*
            List<Shelter> shelters = await _context.Shelters.ToListAsync();
            return shelters.Select(shelter => shelter.ToShelterDto()).ToList();
            */
        }

        // GET: api/Shelter/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ShelterDetailDto>> GetShelter(int id)
        {
            var shelter = await _context.Shelters.FindAsync(id);

            if (shelter == null)
            {
                return NotFound();
            }

            return shelter.ToShelterDetailDto();
        }

        // PUT: api/Shelter/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutShelter(int id, ShelterUpdateDto shelterDto)
        {
            if (id != shelterDto.Id)
            {
                return BadRequest();
            }

            Shelter? shelter = await _context.Shelters.FindAsync(id);

            if (shelter == null)
            {
                return NotFound();
            }
            
            _context.Entry(shelter).CurrentValues.SetValues(shelterDto);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShelterExists(id))
                {
                    return NotFound();
                }

                throw;
            }
            return NoContent();
        }

        // POST: api/Shelter
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Shelter>> PostShelter(ShelterCreateDto shelterDto)
        {
            Shelter shelter = shelterDto.ToShelter();
            _context.Shelters.Add(shelter);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetShelter", new { id = shelter.Id }, shelter);
        }

        // DELETE: api/Shelter/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShelter(int id)
        {
            var shelter = await _context.Shelters.FindAsync(id);
            if (shelter == null)
            {
                return NotFound();
            }

            _context.Shelters.Remove(shelter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShelterExists(int id)
        {
            return _context.Shelters.Any(e => e.Id == id);
        }
    }
}
