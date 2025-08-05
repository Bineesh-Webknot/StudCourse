using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.AspNetCore.Mvc;

namespace StudCourseApp1.Config;

public static class ApiBehaviourConfig
{
    public static void ConfigApiBehaviour(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp =>
                        {
                            StringBuilder sb = new StringBuilder();
                            foreach (var valueError in kvp.Value.Errors)
                            {
                                sb.Append(valueError.ErrorMessage);
                            }

                            return sb;
                        });
                StringBuilder sb =  new StringBuilder();
                foreach (var error in errors)
                {
                    sb.Append(error.Key + " : " +error.Value + " ");
                }
                throw new ValidationException(sb.ToString());
            };
        });
    }
}