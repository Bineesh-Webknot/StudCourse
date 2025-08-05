using Microsoft.AspNetCore.Mvc;

namespace StudCourseApp1.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    
    [HttpGet("index")]
    public IActionResult Index()
    {
        return Ok("hello world");
    }
}