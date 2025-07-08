using System;

namespace API.DTOs;

public class UserDto
{
    public required string Id { get; set; }
    public required string UserEmail { get; set; }
    public required string DisplayName { get; set; }
    public required string Username { get; set; }
    public required string Token { get; set; }
    public string? ImageUrl { get; set; }
}
