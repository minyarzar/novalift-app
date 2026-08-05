using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NovaLift.Application.DTOs;
using NovaLift.Application.Interfaces;
using System.Security.Claims;

namespace NovaLift.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;


    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }


    // POST: api/auth/register
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register(
        [FromBody] RegisterRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        var result =
            await _authService.RegisterAsync(request);


        if (!result.Success)
            return BadRequest(result);


        return Ok(result);
    }



    // POST: api/auth/login
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(
        [FromBody] LoginRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);


        var result =
            await _authService.LoginAsync(request);


        if (!result.Success)
            return Unauthorized(result);


        return Ok(result);
    }



    // GET: api/auth/me
    [HttpGet("me")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUser()
    {

        var userId = GetUserId();


        if(userId == null)
            return Unauthorized();



        var result =
            await _authService
            .GetCurrentUserAsync(userId.Value);



        return Ok(result);
    }




    private int? GetUserId()
    {
        var claim =
        User.FindFirst(
            ClaimTypes.NameIdentifier
        );


        if(claim == null)
            return null;


        return int.TryParse(
            claim.Value,
            out int id
        )
        ? id
        : null;
    }
}