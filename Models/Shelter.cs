namespace AdoptAPet.Models;

public class Shelter
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Address { get; set; }
    
    public string PhoneNumber { get; set; }
    
    
    // One-to-many relationship: One shelter can house many pets
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}