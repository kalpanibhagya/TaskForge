using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskForge.Authentication.Database;
using TaskForge.Authentication.Services;

namespace TaskForge.Authentication.Service.Controllers;

[ApiController]
[Route("Auth")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthService _authenticationService;

    public AuthenticationController(IAuthService auth)
    {
        _authenticationService = auth;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterRequest request)
    {
        await _authenticationService.RegisterAsync(request.Username, request.Email, 
            request.Password);
        return Ok(new { message = "Registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var result = await _authenticationService.LoginAsync(request.Username, 
            request.Password);
        return Ok(result.AccessToken);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenApiModel request)
    {
        var tokens = await _authenticationService.RefreshTokenAsync(request);
        return Ok(tokens);
    }

    [HttpPost("revoke")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> Revoke()
    {
        var username = User.Identity.Name;
        var result = _authenticationService.Revoke(username);
        if (result)
        {
            return Ok(new { message = "Token revoked" });
        }
        else
        {
            return BadRequest(new { message = "Token revocation failed" });
        }
    }
}


