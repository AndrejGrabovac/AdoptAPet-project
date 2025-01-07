using System.ComponentModel.DataAnnotations;

namespace AdoptAPet.DTOs.Breed;

public class BreedCreateDto
{
    [Required]
    public string Name { get; set; } = null!;
}