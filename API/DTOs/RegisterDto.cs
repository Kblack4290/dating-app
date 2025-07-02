using System.ComponentModel.DataAnnotations;

namespace API;

public class RegisterDto
{
    [Required]
    public required string DisplayName { get; set; }
    [Required]
    public required string Email { get; set; }
    [Required]
    public required string Username { get; set; }

    [Required]
    public required string Password { get; set; }
}