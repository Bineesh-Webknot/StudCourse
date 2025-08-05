
using Microsoft.AspNetCore.Mvc;
using StudCourseApp1.Dto;
using StudCourseApp1.Services;

namespace StudCourseApp1.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto model)
    {
        GenericResponse<string> response = await _authService.Register(model);
        if(response.StatusCode.Equals(200))
            return Ok(response);
        if(response.StatusCode.Equals(400))
            return BadRequest(response);
        if(response.StatusCode.Equals(500))
            return StatusCode(500);
        return Empty;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto model)
    {
       GenericResponse<string> response = await _authService.Login(model);
       if(response.StatusCode.Equals(200))
            return Ok(response);
       if(response.StatusCode.Equals(400))
           return BadRequest(response);
       return Empty;
    }
}