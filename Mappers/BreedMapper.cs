using AdoptAPet.DTOs.Breed;
using AdoptAPet.Models;

namespace AdoptAPet.Mappers;

public static class BreedMapper
{
    public static BreedDto ToBreedDto(this Breed breed)
    {
        return new BreedDto()
        {
            Id = breed.Id,
            Name = breed.Name
        };
    }

    public static Breed ToBreed(this BreedCreateDto dto)
    {
        return new Breed()
        {
            Name = dto.Name
        };
    }

    public static Breed ToBreed(this BreedUpdateDto dto)
    {
        return new Breed()
        {
            Id = dto.Id,
            Name = dto.Name
        };
    }
}