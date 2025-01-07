namespace AdoptAPet.Models;

public class Breed
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    // One-to-Many relationship: One breed can be associated with many pets
    public ICollection<Pet> Pets { get; set; } = new List<Pet>();
}