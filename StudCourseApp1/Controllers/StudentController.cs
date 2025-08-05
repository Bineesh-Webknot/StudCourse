using Microsoft.AspNetCore.Mvc;
using StudCourseApp1.Services;

namespace StudCourseApp1.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController :  ControllerBase
{
    private readonly StudentService _studentService;

    public StudentController(StudentService studentService)
    {
        _studentService = studentService;
    }
    
    [HttpGet("list")]
    public IActionResult GetAllStudents()
    {
        var result = _studentService.GetAllStudents();
        return Ok(result.Result);
    }
    
    [HttpGet]
    public IActionResult GetStudentById([FromQuery] int id)
    {
        var result =  _studentService.GetStudentById(id);
        return Ok(result.Result);
    }
    
}