using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudCourseApp1.Dto;
using StudCourseApp1.Exceptions;
using StudCourseApp1.Repo;

namespace StudCourseApp1.Services;

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
        var students = await _dbContext.Students
            .Include(s => s.User)
            .ToListAsync();
        var result = _mapper.Map<List<StudentDto>>(students);
        return result;
    }
    
    public async Task<StudentDto> GetStudentById(int id)
    {
        var student = await _dbContext.Students
            .Include(s => s.User)
            .FirstOrDefaultAsync(c => c.Id == id);
        if (student == null)
        {
            throw new DataNotFoundException("Student not found");
        }
        return _mapper.Map<StudentDto>(student);
    }
    
}