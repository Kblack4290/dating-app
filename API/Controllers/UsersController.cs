using System;
using API.Data;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class UsersController(IMemberRepository memberRepository) : BaseApiController
{
[Authorize]

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Member>>> GetUsers()
    {

        return Ok(await memberRepository.GetMembersAsync());
    }



    [HttpGet("{id}")]
    public async Task<ActionResult<Member>> GetUsers(string id)
    {
        var user = await memberRepository.GetMemberByIdAsync(id);

        if (user == null) return NotFound();

        return user;
    }

    [HttpGet("{id}/photos")]

    public async Task<ActionResult<IReadOnlyList<Photo>>> GetUserPhotos(string id)
    {
        return Ok(await memberRepository.GetPhotosForMemberAsync(id));
    } 
}
