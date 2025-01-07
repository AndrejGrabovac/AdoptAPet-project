using System.ComponentModel.DataAnnotations;

namespace AdoptAPet.DTOs.AdoptionRequest;

public class AdoptionRequestCreateDto
{
    [Required]
    public DateTime Date { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    public int PetId { get; set; }
}