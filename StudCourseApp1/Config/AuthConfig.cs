using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StudCourseApp1.Dto;

namespace StudCourseApp1.Config;

public static class AuthConfig
{
    public static void ConfigAuth(this IServiceCollection services, IConfiguration configuration)
    {
        var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]);
        services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        options.Events = new JwtBearerEvents
        {
            OnChallenge = context =>
            {
                context.HandleResponse();
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var response = JsonSerializer.Serialize(new
                    GenericResponse<string>(){
                        Status = "failed",
                        StatusCode = 401,
                        Data = "You are not authorized to access this resource."
                    });
                return context.Response.WriteAsync(response);
            },
            OnAuthenticationFailed = context =>
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "application/json";
                var response = JsonSerializer.Serialize(new
                GenericResponse<string>(){
                    Status = "failed",
                    StatusCode = 401,
                    Data = "You are not authorized to access this resource."
                });
                return context.Response.WriteAsync(response);
            },
            OnForbidden = context =>
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                context.Response.ContentType = "application/json";

                var response = JsonSerializer.Serialize(new
                GenericResponse<string>(){
                    Status = "failed",
                    StatusCode = 403,
                    Data = "You do not have permission to perform this action."
                });

                return context.Response.WriteAsync(response);
            }
        };
    });
    }
}