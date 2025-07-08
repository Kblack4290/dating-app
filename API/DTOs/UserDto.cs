using System;

namespace API.DTOs;

public class UserDto
{

    public required string UserEmail { get; set; }
    public required string Username { get; set; }
    public required string Token { get; set; }
}
