using System.ComponentModel.DataAnnotations;

namespace StudCourseApp1.Dto;

public class CourseDto
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Course name is required")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Course description is required")]
    public string Description { get; set; }
}