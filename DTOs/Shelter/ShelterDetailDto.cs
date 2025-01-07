namespace AdoptAPet.DTOs.Shelter;

public class ShelterDetailDto
{
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Address { get; set; } = null!;
    
    public string PhoneNumber { get; set; } = null!;
}