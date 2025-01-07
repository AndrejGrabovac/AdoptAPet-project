using System.ComponentModel.DataAnnotations;

namespace AdoptAPet.DTOs.Pet;

public class PetUpdateDto
{
    public int Id { get; set; }
    
    [StringLength(30)]
    public string Name { get; set; } = null!;
    
    [Range(0,25)]
    public int Age { get; set; }
    
    public string? Description { get; set; }
    
    public bool IsAdopted { get; set; }
    
    public int ShelterId { get; set; }
    
    public int BreedId { get; set; }
}