namespace AdoptAPet.Models;

public class Pet
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public int Age { get; set; }
    
    public string Description { get; set; }

    public bool IsAdopted { get; set; }
    
    
    // Many-to-One relationship: One shelter can house many pets
    public int ShelterId { get; set; }
    public Shelter Shelter { get; set; }
    
    public int BreedId { get; set; }
    public Breed Breed { get; set; }
    
    // Many-to-Many relationship: A pet can have many adoption requests
    public ICollection<AdoptionRequest> AdoptionRequests { get; set; } = new List<AdoptionRequest>();
}