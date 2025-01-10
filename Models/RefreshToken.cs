namespace AdoptAPet.Models;

public class RefreshToken
{
    public int Id { get; set; }
    
    public required string Value { get; set; }
    
    public DateTime ExpiresAt { get; set; }
}