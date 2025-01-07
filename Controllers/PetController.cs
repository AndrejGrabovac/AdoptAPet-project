
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdoptAPet.Data;
using AdoptAPet.DTOs.Pet;
using AdoptAPet.Mappers;
using AdoptAPet.Models;

namespace AdoptAPet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetController : ControllerBase
    {
        private readonly DataContext _context;

        public PetController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Pet
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PetDto>>> GetPets(int page = 1, int pageSize = 5)
        {
            var pets = await _context.Pets
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return Ok(pets.Select(pet => pet.ToPetDto()).ToList());
            
            /*
            List<Pet> pets = await _context.Pets.ToListAsync();
            return pets.Select(pet => pet.ToPetDto()).ToList();
            */
        }

        // GET: api/Pet/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PetDetailDto>> GetPet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet == null)
            {
                return NotFound();
            }

            return pet.ToPetDetailDto();
        }

        // PUT: api/Pet/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPet(int id, PetUpdateDto petDto)
        {
            if (id != petDto.Id)
            {
                return BadRequest();
            }

            Pet? pet = await _context.Pets.FindAsync(id);
            
            if (pet == null)
            {
                return NotFound();
            }
            
            _context.Entry(pet).CurrentValues.SetValues(petDto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PetExists(id))
                {
                    return NotFound();
                }
                throw;
            }
            return NoContent();
        }

        // POST: api/Pet
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pet>> PostPet(PetCreateDto petDto)
        {   
            //Check if the shelterId exist
            var shelter = await _context.Shelters.FindAsync(petDto.ShelterId);
            if (shelter == null)
            {
                return BadRequest("Shelter does not exist");
            }
            
            
            Pet pet = petDto.ToPet();
            _context.Add(pet);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPet", new { id = pet.Id }, pet);
        }

        // DELETE: api/Pet/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);
            if (pet == null)
            {
                return NotFound();
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PetExists(int id)
        {
            return _context.Pets.Any(e => e.Id == id);
        }
    }
}
