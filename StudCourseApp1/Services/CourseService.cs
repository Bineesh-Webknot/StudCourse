using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudCourseApp1.Dto;
using StudCourseApp1.Exceptions;
using StudCourseApp1.Models;
using StudCourseApp1.Repo;

namespace StudCourseApp1.Services;

public class CourseService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public CourseService(ApplicationDbContext dbContext,  IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public CourseDto AddCourse(CourseDto course)
    {
        var newCourse = _dbContext.Courses.AddAsync(new Course()
        {
            Id = course.Id,
            Name = course.Name,
            Description = course.Description
        });
        _dbContext.SaveChanges();
        return _mapper.Map<CourseDto>(newCourse.Result.Entity);
    }

    public async Task<List<CourseDto>> GetAllCourses()
    {
        var courses = await _dbContext.Courses.ToListAsync();
        var result = _mapper.Map<List<CourseDto>>(courses);
        return result;
    }
    
    public async Task<CourseDto> GetCourseById(int id)
    {
        var course = await _dbContext.Courses.FirstAsync(c => c.Id == id);
        return _mapper.Map<CourseDto>(course);
    }
    
    public async Task<CourseDto> UpdateCourse(int id, CourseDto courseDto)
    {
        var courseToUpdate = await _dbContext.Courses.FirstAsync(c => c.Id == id);
        if (courseToUpdate == null)
        {
            throw new DataNotFoundException("Course not found");
        }
        courseToUpdate = _mapper.Map(courseDto, courseToUpdate);
        var course =  _dbContext.Courses.Update(courseToUpdate);
        await _dbContext.SaveChangesAsync();
        return _mapper.Map<CourseDto>(course.Entity);
    }
    
    public async Task<string> DeleteCourse(int id)
    {
        var course = await _dbContext.Courses.FindAsync(id);
        if (course == null)
        {
            throw new DataNotFoundException("Course not found");
        }
        _dbContext.Courses.Remove(course);
        await _dbContext.SaveChangesAsync();
        return "Deleted";
    }
    
    
}