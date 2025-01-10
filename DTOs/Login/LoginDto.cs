using System.ComponentModel.DataAnnotations;

namespace AdoptAPet.DTOs.Login;

public class LoginDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    
    [Required]
    [StringLength(30, MinimumLength = 8)]
    public required string Password { get; set; }
}