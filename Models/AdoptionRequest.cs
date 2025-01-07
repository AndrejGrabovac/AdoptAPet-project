namespace AdoptAPet.Models;

public class AdoptionRequest
{
    public int Id { get; set; }
    
    public DateTime Date { get; set; }
    
    public bool IsApproved { get; set; }
    
    // Many-to-One relationship: A user can make many adoption requests
    public int UserId { get; set; }
    public User User { get; set; }
    
    // Many-to-One relationship: A pet can have many adoption requests
    public int PetId { get; set; }
    public Pet Pet { get; set; }
}