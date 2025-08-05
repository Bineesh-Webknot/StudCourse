namespace StudCourseApp1.Dto;

public class StudentWithCoursesDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Dept { get; set; }
    public CourseDto [] EnrolledCourses { get; set; }
}