using System.ComponentModel.DataAnnotations;

namespace AdoptAPet.DTOs.Shelter;

public class ShelterUpdateDto
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = null!;
    
    [Required]
    [StringLength(50)]
    public string Address { get; set; } = null!;
    
    [Phone]
    public string? PhoneNumber { get; set; }
}