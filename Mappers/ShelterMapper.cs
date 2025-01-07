using AdoptAPet.DTOs.Shelter;
using AdoptAPet.Models;

namespace AdoptAPet.Mappers;

public static class ShelterMapper
{
    public static ShelterDto ToShelterDto(this Shelter shelter)
    {
        return new ShelterDto()
        {
            Id = shelter.Id,
            Name = shelter.Name
        };
    }
    
    public static ShelterDetailDto ToShelterDetailDto(this Shelter shelter)
    {
        return new ShelterDetailDto()
        {
            Id = shelter.Id,
            Name = shelter.Name,
            Address = shelter.Address,
            PhoneNumber = shelter.PhoneNumber
        };
    }

    public static Shelter ToShelter(this ShelterCreateDto dto)
    {
        return new Shelter()
        {
            Name = dto.Name,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber
        };
    }
    
    public static Shelter ToShelter(this ShelterUpdateDto dto)
    {
        return new Shelter()
        {   
            Id = dto.Id,
            Name = dto.Name,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber
        };
    }
}