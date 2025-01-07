using AdoptAPet.DTOs.Pet;
using AdoptAPet.Models;

namespace AdoptAPet.Mappers;

public static class PetMapper
{
    public static PetDto ToPetDto(this Pet pet)
    {
        return new PetDto()
        {
            Id = pet.Id,
            Name = pet.Name
        };
    }

    public static PetDetailDto ToPetDetailDto(this Pet pet)
    {
        return new PetDetailDto()
        {
            Id = pet.Id,
            Name = pet.Name,
            Age = pet.Age,
            Description = pet.Description,
            IsAdopted = pet.IsAdopted,
            ShelterId = pet.ShelterId,
            BreedId = pet.BreedId
        };
    }

    public static Pet ToPet(this PetCreateDto dto)
    {
        return new Pet()
        {
            Name = dto.Name,
            Age = dto.Age,
            Description = dto.Description,
            IsAdopted = false,
            ShelterId = dto.ShelterId,
            BreedId = dto.BreedId
        };
    }

    public static Pet ToPet(this PetUpdateDto dto)
    {
        return new Pet()
        {
            Id = dto.Id,
            Name = dto.Name,
            Age = dto.Age,
            Description = dto.Description,
            IsAdopted = dto.IsAdopted,
            ShelterId = dto.ShelterId,
            BreedId = dto.BreedId
        };
    }
}