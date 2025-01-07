using System.ComponentModel.DataAnnotations;

namespace AdoptAPet.DTOs.AdoptionRequest;

public class AdoptionRequestUpdateDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    public bool IsApproved { get; set; }
}