namespace StudCourseApp1.Dto;

public class CourseWithStudentsDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public StudentDto [] EnrolledStudents { get; set; }
}