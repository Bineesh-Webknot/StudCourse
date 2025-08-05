using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudCourseApp1.Models;
using StudCourseApp1.Repo;

namespace StudCourseApp1.Config;

public static class DbConfig
{
    public static void ConfigDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
    }
}