namespace AdoptAPet.DTOs.AdoptionRequest;

public class AdoptionRequestDetailDto
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public bool IsApproved { get; set; }

    public int UserId { get; set; }

    public int PetId { get; set; }
}