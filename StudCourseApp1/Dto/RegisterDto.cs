using System.ComponentModel.DataAnnotations;

namespace StudCourseApp1.Dto;

public class RegisterDto
{
    [Required(ErrorMessage = "User name is required")]
    public string Username { get; set; }
    
    [EmailAddress(ErrorMessage = "Email is invalid")]
    public string Email { get; set; }
    
    [MaxLength(20, ErrorMessage = "Password must be less than 20 characters")]
    [MinLength(7, ErrorMessage = "Password must be least 7 characters")]
    public string Password { get; set; }
    
    public string Dept { get; set; }
}