using System;
using API.DTOs;
using API.Entities;
using API.Interfaces;
namespace API.Extensions;

public static class AppUserExtensions
{
    public static UserDto ToDTO(this AppUser user, ITokenService tokenService)
    {
        return new UserDto
        {
            Id = user.Id,
            UserEmail = user.Email,
            DisplayName = user.DisplayName,
            Username = user.Username,
            ImageUrl = user.ImageUrl,
            Token = tokenService.CreateToken(user)

        };
    }

}

