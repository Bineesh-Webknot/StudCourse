using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudCourseApp1.Dto;
using StudCourseApp1.Exceptions;
using StudCourseApp1.Models;
using StudCourseApp1.Repo;

namespace StudStudentApp1;

public class StudentService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public StudentService(ApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<List<StudentDto>> GetAllStudents()
    {
        var Students = await _dbContext.Students
            .Include(s => s.User)
            .ToListAsync();
        var result = _mapper.Map<List<StudentDto>>(Students);
        return result;
    }
    
    public async Task<StudentDto> GetStudentById(int id)
    {
        var Student = await _dbContext.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (Student == null)
        {
            throw new DataNotFoundException("Student not found");
        }
        return _mapper.Map<StudentDto>(Student);;
    }
    
}