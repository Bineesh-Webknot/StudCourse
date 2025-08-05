using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using StudCourseApp1.Dto;
using StudCourseApp1.Models;
using StudCourseApp1.Repo;

namespace StudCourseApp1.Services;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IConfiguration _config;
    private readonly ApplicationDbContext _dbContext;

    public AuthService(UserManager<ApplicationUser> userManager,
        RoleManager<IdentityRole> roleManager, IConfiguration config,  ApplicationDbContext dbContext)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _config = config;
        _dbContext = dbContext;
    }
    
    public async Task<GenericResponse<string>> Register(RegisterDto model)
    {
        var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (!result.Succeeded)
            return new GenericResponse<string>
            {
                Status = "Failure",
                StatusCode = StatusCodes.Status400BadRequest,
                Data = result.Errors.First().Description
            };

        await _roleManager.CreateAsync(new IdentityRole("STUDENT"));

        await _userManager.AddToRoleAsync(user, "STUDENT");
        var student = new Student()
        {
            ApplicationUserId = user.Id,
            Dept = model.Dept
        };
        _dbContext.Students.Add(student);
        await _dbContext.SaveChangesAsync();
        return new GenericResponse<string>
        {
            Status = "Success",
            StatusCode = StatusCodes.Status200OK,
            Data = "User registered."
        };
    }
    
    public async Task<GenericResponse<string>> Login(LoginDto model)
    {
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
        {
            var roles = await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Secret"]);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Role, roles.First()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new GenericResponse<string>
            {
                Status = "Success",
                StatusCode = StatusCodes.Status200OK,
                Data = tokenHandler.WriteToken(token)
            };
        }

        throw new UnauthorizedAccessException("Unauthorized access");
    }

}