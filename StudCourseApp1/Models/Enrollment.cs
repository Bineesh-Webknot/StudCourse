namespace StudCourseApp1.Models;

public class Enrollment
{
    public int StudentId { get; set; }
    public Student Student { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; }
    public int EnrollmentId { get; set; }
    public DateTime EnrollmentDate { get; set; }
}