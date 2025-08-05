using System.Security.Claims;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudCourseApp1.Dto;
using StudCourseApp1.Models;
using StudCourseApp1.Repo;

namespace StudCourseApp1.Services;

public class EnrollmentService
{
    
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public EnrollmentService(ApplicationDbContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }
    
    public async Task<string> Enroll(List<int> courseIds)
    {
        var userContext = _httpContextAccessor.HttpContext?.User;
        if (userContext == null || userContext.Identity == null || !userContext.Identity.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("User not logged in");
        }
        var id = userContext.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id == null)
        {
            throw new UnauthorizedAccessException("User not logged in; Please Login first");
        }
        var student = await _context.Students.FirstAsync(s => s.ApplicationUserId.Equals(id));
        if (student == null)
            throw new Exception("Student not found");

        var coursesEnrolled = await _context.Courses
            .Where(c => courseIds.Contains(c.Id))
            .ToListAsync();

        if (coursesEnrolled.Count != courseIds.Count)
            throw new BadHttpRequestException("Courses not Found");

        foreach (var course in coursesEnrolled)
        {
            var exists = await _context.Enrollments
                .AnyAsync(e => e.StudentId == student.Id && e.CourseId == course.Id);

            if (!exists)
            {
                var enrollment = new Enrollment
                {
                    StudentId = student.Id,
                    CourseId = course.Id,
                    EnrollmentDate = DateTime.UtcNow,
                };
                _context.Enrollments.Add(enrollment);
            }
        }

        await _context.SaveChangesAsync();

        return "Enrolled Successfully";
    }

    public async Task<List<StudentWithCoursesDto>> EnrolledStudents()
    {   
        var students = await _context.Students
            .Include(c => c.User)
            .Include(c => c.Enrollments)
            .ThenInclude(e => e.Course)
            .ToListAsync();
        return _mapper.Map<List<StudentWithCoursesDto>>(students);
    }

    public async Task<List<CourseWithStudentsDto>> CoursesEnrolled()
    {
        var courses = await _context.Courses
            .Include(c => c.Enrollments)
            .ThenInclude(e => e.Student)
            .ThenInclude(s => s.User)
            .ToListAsync();
        return _mapper.Map<List<CourseWithStudentsDto>>(courses);
    }
    
    public async Task<Student> EnrolledStudentById(int? id)
    {
        if (id == null)
        {
            var userContext = _httpContextAccessor.HttpContext?.User;
            if (userContext == null || userContext.Identity == null || !userContext.Identity.IsAuthenticated)
            {
                throw new UnauthorizedAccessException("User not logged in");
            }
            var userId = userContext.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new UnauthorizedAccessException("User not logged in; Please Login first");
            }
            var loginStudent = await _context.Students.FirstAsync(s => s.ApplicationUserId.Equals(id));
            if (loginStudent == null)
                throw new Exception("Student not found");
            id = loginStudent.Id;
        }
        var student =  await _context.Students.Include(c => c.Enrollments)
            .ThenInclude(e => e.Course)
            .FirstAsync(s => s.Id == id);
        return student;
    }

    public async Task<Course> CoursesEnrolledById(int id)
    {
        var course = await _context.Courses.Include(c => c.Enrollments)
            .ThenInclude(e => e.Student)
            .FirstAsync(c => c.Id == id);
        return course;
    }
}