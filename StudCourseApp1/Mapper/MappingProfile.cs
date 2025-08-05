using AutoMapper;
using StudCourseApp1.Dto;
using StudCourseApp1.Models;

namespace StudCourseApp1.Mapper;

public class MappingProfile : Profile
{

    public MappingProfile()
    {
        CreateMap<Student, StudentDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.UserName));
        CreateMap<Course, CourseDto>().ReverseMap();
        
        CreateMap<Student, StudentWithCoursesDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.EnrolledCourses, opt =>
                opt.MapFrom(src => src.Enrollments.Select(e => e.Course)));
        
        CreateMap<Course, CourseWithStudentsDto>()
            .ForMember(dest => dest.EnrolledStudents, opt =>
                opt.MapFrom(src => src.Enrollments.Select(e => e.Student)));

    }
    
}