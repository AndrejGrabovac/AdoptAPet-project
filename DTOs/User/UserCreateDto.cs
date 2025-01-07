using System.ComponentModel.DataAnnotations;

namespace AdoptAPet.DTOs.User;

public class UserCreateDto
{
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = null!;
    
    [Required]
    [EmailAddress]
    public string Email { get; set; } = null!;
    
    [Required]
    [StringLength(30, MinimumLength = 8)]
    public string Password { get; set; } = null!;
    
    [Phone]
    public string? PhoneNumber { get; set; }
}