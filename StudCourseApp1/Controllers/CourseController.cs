using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudCourseApp1.Dto;
using StudCourseApp1.Models;

namespace StudCourseApp1.Controllers;

[ApiController]
[Route("[controller]")]
public class CourseController :  ControllerBase
{
    
    public readonly CourseService _courseService;

    public CourseController(CourseService courseService)
    {
        _courseService = courseService;
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpPost("add")]
    public IActionResult AddCourse([FromBody] CourseDto courseDto)
    {
        var result = _courseService.addCourse(courseDto);
        return Ok(result);
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpPut("update")]
    public IActionResult AddCourse([FromQuery] int id, [FromBody] CourseDto courseDto)
    {
        var result = _courseService.UpdateCourse(id,courseDto);
        return Ok(result.Result);
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpDelete("del")]
    public IActionResult AddCourse([FromQuery] int id)
    {
        var result = _courseService.DeleteCourse(id);
        return Ok(result.Result);
    }
    
    [Authorize]
    [HttpGet("list")]
    public IActionResult GetAllCourses()
    {
        var result = _courseService.GetAllCourses();
        return Ok(result.Result);
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult GetCourseById([FromQuery] int id)
    {
        var result =  _courseService.GetCourseById(id);
        return Ok(result.Result);
    }
    
}