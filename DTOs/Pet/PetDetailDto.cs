namespace AdoptAPet.DTOs.Pet;

public class PetDetailDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public int Age { get; set; }
    
    public string Description { get; set; } = null!;
    
    public bool IsAdopted { get; set; }
    
    public int ShelterId { get; set; }
    
    public int BreedId { get; set; }
}