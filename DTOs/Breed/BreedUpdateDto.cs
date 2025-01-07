using System.ComponentModel.DataAnnotations;

namespace AdoptAPet.DTOs.Breed;

public class BreedUpdateDto
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = null!;
}