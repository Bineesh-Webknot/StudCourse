using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;
using StudCourseApp1.Dto;

namespace StudCourseApp1.Exceptions;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unhandled Exception: {ex.Message}");
            var actualException = ex is AggregateException aggEx
                ? aggEx.Flatten().InnerExceptions.FirstOrDefault() ?? ex
                : ex;
            await HandleExceptionAsync(context, actualException);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var statusCode = exception switch
        {
            DataNotFoundException => (int)HttpStatusCode.NotFound,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            BadHttpRequestException  => (int)HttpStatusCode.BadRequest,
            ValidationException  => (int)HttpStatusCode.BadRequest,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var response = new GenericResponse<string>()
        {
            StatusCode = statusCode,
            Data = exception.Message,
            Status = "failed"
        };
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}