
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdoptAPet.Data;
using AdoptAPet.DTOs.Breed;
using AdoptAPet.Mappers;
using AdoptAPet.Models;

namespace AdoptAPet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreedController : ControllerBase
    {
        private readonly DataContext _context;

        public BreedController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Breed
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreedDto>>> GetBreeds(int page = 1, int pageSize = 5)
        {   
            var breeds = await _context.Breeds
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(breeds.Select(breed => breed.ToBreedDto()).ToList());
            /*
            List<Breed> breeds = await _context.Breeds.ToListAsync();
            return breeds.Select(breed => breed.ToBreedDto()).ToList();
            */
        }

        // GET: api/Breed/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BreedDto>> GetBreed(int id)
        {
            var breed = await _context.Breeds.FindAsync(id);

            if (breed == null)
            {
                return NotFound();
            }

            return breed.ToBreedDto();
        }

        // PUT: api/Breed/5
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBreed(int id, BreedUpdateDto breedDto)
        {
            if (id != breedDto.Id)
            {
                return BadRequest();
            }

            Breed? breed = await _context.Breeds.FindAsync(id);

            if (breed == null)
            {
                return NotFound();
            }
            
            _context.Entry(breed).CurrentValues.SetValues(breedDto);
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreedExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Breed
        // To protect from over posting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Breed>> PostBreed(BreedCreateDto breedDto)
        {
            Breed breed = breedDto.ToBreed();
            _context.Breeds.Add(breed);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBreed", new { id = breed.Id }, breed);
        }

        // DELETE: api/Breed/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBreed(int id)
        {
            var breed = await _context.Breeds.FindAsync(id);
            if (breed == null)
            {
                return NotFound();
            }

            _context.Breeds.Remove(breed);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BreedExists(int id)
        {
            return _context.Breeds.Any(e => e.Id == id);
        }
    }
}
