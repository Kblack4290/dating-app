using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;


public class AccountController(DataContext context, ITokenService tokenService) : BaseApiController
{
    [HttpPost("register")]
    public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
    {
        using var hmac = new HMACSHA512();

        if (await UserPropertyExists("username", registerDto.Username)) return BadRequest("Username is taken");
        if (await UserPropertyExists("email", registerDto.Email)) return BadRequest("Email is already registered");
        
        var user = new AppUser
        {
            DisplayName = registerDto.DisplayName,
            Email = registerDto.Email,
            Username = registerDto.Username.ToLower(),
            PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
            PasswordSalts = hmac.Key
        };

        context.Users.Add(user);
        await context.SaveChangesAsync();

        return new UserDto
        {
            UserEmail = user.Email,
            Username = user.Username,
            Token = tokenService.CreateToken(user)
        };
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
    {
        var identifier = loginDto.Identifier.ToLower();

        var user = await context.Users
        .FirstOrDefaultAsync(x => x.Username.ToLower() == identifier || x.Email.ToLower() == identifier);


        if (user == null ) return Unauthorized("Invalid username or email");

        using var hmac = new HMACSHA512(user.PasswordSalts);

        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

        for (int i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
        }

        return new UserDto
        {   
            UserEmail = user.Email,
            Username = user.Username,
            Token = tokenService.CreateToken(user)
        };
    }
    private async Task<bool> UserPropertyExists(string property, string value)
    {
        return property.ToLower() switch
        {
            "username" => await context.Users.AnyAsync(x => x.Username.ToLower() == value.ToLower()),
            "email" => await context.Users.AnyAsync(x => x.Email.ToLower() == value.ToLower()),
            _ => throw new ArgumentException("Invalid property name")
        };
    }
}
