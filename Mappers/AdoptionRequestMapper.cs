using AdoptAPet.DTOs.AdoptionRequest;
using AdoptAPet.Models;

namespace AdoptAPet.Mappers;

public static class AdoptionRequestMapper
{
    public static AdoptionRequestDto ToAdoptionRequestDto(this AdoptionRequest adoptionRequest)
    {
        return new AdoptionRequestDto()
        {
            Id = adoptionRequest.Id,
            Date = adoptionRequest.Date,
            IsApproved = adoptionRequest.IsApproved
        };
    }

    public static AdoptionRequestDetailDto ToAdoptionRequestDetailDto(this AdoptionRequest adoptionRequest)
    {
        return new AdoptionRequestDetailDto()
        {
            Id = adoptionRequest.Id,
            Date = adoptionRequest.Date,
            IsApproved = adoptionRequest.IsApproved,
            UserId = adoptionRequest.UserId,
            PetId = adoptionRequest.PetId
        };
    }

    public static AdoptionRequest ToAdoptionRequest(this AdoptionRequestCreateDto dto)
    {
        return new AdoptionRequest()
        {   
            Date = dto.Date,
            UserId = dto.UserId,
            PetId = dto.PetId
        };
    }

    public static AdoptionRequest ToAdoptionRequest(this AdoptionRequestUpdateDto dto)
    {
        return new AdoptionRequest()
        {
            Id = dto.Id,
            Date = dto.Date,
            IsApproved = dto.IsApproved
        };
    }
}