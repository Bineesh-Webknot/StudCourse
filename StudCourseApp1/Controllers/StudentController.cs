using Microsoft.AspNetCore.Mvc;
using StudCourseApp1.Dto;

namespace StudStudentApp1.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController :  ControllerBase
{
      
    public readonly StudentService _StudentService;

    public StudentController(StudentService StudentService)
    {
        _StudentService = StudentService;
    }
    
    [HttpGet("list")]
    public IActionResult GetAllStudents()
    {
        var result = _StudentService.GetAllStudents();
        return Ok(result.Result);
    }
    
    [HttpGet]
    public IActionResult GetStudentById([FromQuery] int id)
    {
        var result =  _StudentService.GetStudentById(id);
        return Ok(result.Result);
    }
    
}