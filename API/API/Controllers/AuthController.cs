using API.Dto;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            var response = await _authService.AuthenticateAsync(request.Username, request.Password, request.Role);
            if (response == null) return Unauthorized("Invalid credentials or role");

            return Ok(response);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
        }
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        try
        {
            var result = await _authService.ResetPasswordAsync(request.UserId, request.NewPassword);
            if (!result) return BadRequest("Password reset failed");

            return Ok("Password reset successful");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "An error occurred while processing the request: " + ex.Message);
        }
    }
}