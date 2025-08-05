using Microsoft.AspNetCore.Identity;
using StudCourseApp1.Models;

namespace StudCourseApp1.Services;

public static class AppDataInitializer
{
    public static async Task Initialize(this IApplicationBuilder app)
    {
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            var adminEmail = "admin@gmail.com";
            var adminUserName = "Admin";
            var adminRole = "ADMIN";
            var adminPassword = "Admin@123";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                await roleManager.CreateAsync(new IdentityRole(adminRole));
                var user = new ApplicationUser
                {
                    UserName = adminUserName,
                    Email = adminEmail
                };
                var result = await userManager.CreateAsync(user, adminPassword);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, adminRole);
                }
            }
        }
    }
}