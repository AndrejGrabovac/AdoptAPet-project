using System.ComponentModel.DataAnnotations;

namespace AdoptAPet.DTOs.Register;

public class RegisterDto
{  
    [Required]
    [StringLength(30)]
    public string Name { get; set; }
    
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Phone]
    [Required]
    [RegularExpression(@"^(0\d{8,14}|\+385\d{8,13})$", ErrorMessage = "Invalid phone number format.")]
    public string PhoneNumber { get; set; }
}