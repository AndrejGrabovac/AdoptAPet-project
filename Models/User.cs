namespace AdoptAPet.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string Email { get; set; } 
    
    public string Password { get; set; }
    
    public string? PhoneNumber { get; set; }
    
    // One-to-many relationship: One user can make many adoption requests
    public ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();
    
}