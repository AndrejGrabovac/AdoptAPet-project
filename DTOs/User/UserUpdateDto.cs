using System.ComponentModel.DataAnnotations;

namespace AdoptAPet.DTOs.User;

public class UserUpdateDto
{
    public int Id { get; set; }
    
    [Required]
    [StringLength(30)]
    public string Name { get; set; } = null!;
    
    [Phone]
    public string PhoneNumber { get; set; }
}