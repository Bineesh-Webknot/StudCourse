using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudCourseApp1.Models;
using StudCourseApp1.Services;
using StudStudentApp1;

namespace StudCourseApp1.Controllers;

[ApiController]
[Route("enroll")]
public class EnrollmentController: ControllerBase
{
    
    public readonly EnrollmentService _enrollmentService;

    public EnrollmentController(EnrollmentService enrollmentService)
    {
        _enrollmentService = enrollmentService;
    }
    
    [Authorize]
    [HttpPost]
    public IActionResult Enrollment([FromQuery] List<int> courseIds)
    {
        
        var result = _enrollmentService.Enroll(courseIds);
        return Ok(result.Result);
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpGet("student-enrollment")]
    public IActionResult EnrolledStudents()
    {
        var result = _enrollmentService.EnrolledStudents();
        return Ok(result.Result);
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpGet("course-enrollment")]
    public IActionResult CoursesEnrolled()
    {
        var result = _enrollmentService.CoursesEnrolled();
        return Ok(result.Result);
    }
    
    [Authorize]
    [HttpGet("student")]
    public IActionResult EnrolledStudentsById([FromQuery]int? studentId)
    {
        var result = _enrollmentService.EnrolledStudentById(studentId);
        return Ok(result.Result);
    }
    
    [Authorize(Roles = "ADMIN")]
    [HttpGet("course")]
    public IActionResult CoursesEnrolledbyId([FromQuery]int courseId)
    {
        var result = _enrollmentService.CoursesEnrolledById(courseId);
        return Ok(result.Result);
    }
    
}