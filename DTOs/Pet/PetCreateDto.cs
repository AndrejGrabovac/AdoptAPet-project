using System.ComponentModel.DataAnnotations;

namespace AdoptAPet.DTOs.Pet;

public class PetCreateDto
{
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = null!;
    
    [Required]
    [Range(0, 25)]
    public int Age { get; set; }
    
    [Required]
    [StringLength(250)]
    public string Description { get; set; } = null!;

    [Required]
    public bool IsAdopted { get; set; }
    
    [Required]
    public int ShelterId { get; set; }
    
    [Required]
    public int BreedId { get; set; }
}