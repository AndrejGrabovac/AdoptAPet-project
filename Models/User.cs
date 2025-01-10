namespace AdoptAPet.Models;

public class User
{
    public int Id { get; set; }

    public string Name { get; set; }
    
    public string Email { get; set; } 
    
    //public string Password { get; set; }
    
    public byte[] PasswordHash { get; set; }
    
    public byte[] PasswordSalt { get; set; }
    
    public string PhoneNumber { get; set; }
    
    // One-to-many relationship: One user can make many adoption requests
    public ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();
    
    public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    
    public int RoleId { get; set; }
    public Role Role { get; set; }
    
}