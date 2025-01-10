using AdoptAPet.DTOs.User;
using AdoptAPet.Models;

namespace AdoptAPet.Mappers;

public static class UserMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            Name = user.Name
        };
    }
    
    public static UserDetailDto ToUserDetailDto(this User user)
    {
        return new UserDetailDto()
        {
            Id = user.Id,
            Name = user.Name,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber
        };
    }

    /*
    public static User ToUser(this UserCreateDto dto)
    {
        return new User()
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = dto.Password,
            PhoneNumber = dto.PhoneNumber
        };
    }
    */
    
    public static User ToUser(this UserUpdateDto dto)
    {
        return new User()
        {   
            Id = dto.Id,
            Name = dto.Name,
            PhoneNumber = dto.PhoneNumber
        };
    }
}