
namespace StudCourseApp1.Models;

public class Student
{
    public int Id { get; set; }
    public string Dept { get; set; }
    
    public String ApplicationUserId { get; set; }
    public  ApplicationUser User { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
}