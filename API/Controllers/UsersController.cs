using System;
using System.Security.Claims;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
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

    [HttpPut]

    public async Task<ActionResult> UpdateUser( MemberUpdateDto memberUpdateDto)
    {
        var memberId = User.GetMemberId();

        var member = await memberRepository.GetMemberForUpdate(memberId);
        if (member == null) return BadRequest("Member not found");

        member.DisplayName = memberUpdateDto.DisplayName ?? member.DisplayName;
        member.Description = memberUpdateDto.Description ?? member.Description;
        member.City = memberUpdateDto.City ?? member.City;
        member.Country = memberUpdateDto.Country ?? member.Country;

        member.User.DisplayName = memberUpdateDto.DisplayName ?? member.User.DisplayName;

        memberRepository.Update(member);

        if (await memberRepository.SaveAllAsync()) return NoContent();

        return BadRequest("Failed to update member");
    }
}
