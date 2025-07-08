using System;

namespace API;

public class LoginDto
{
    public required string Identifier { get; set; }
    public required string Password { get; set; }
}
