using StudCourseApp1.Config;
using StudCourseApp1.Exceptions;
using StudCourseApp1.Mapper;
using StudCourseApp1.Services;

var builder = WebApplication.CreateBuilder(args);

// add controllers
builder.Services.AddControllers();
// add services
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<StudentService>();
builder.Services.AddScoped<EnrollmentService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddEndpointsApiExplorer();
// add configs
builder.Services.ConfigSwagger();
builder.Services.ConfigDatabase(builder.Configuration);
builder.Services.ConfigAuth(builder.Configuration);
builder.Services.AddAuthorization();
builder.Services.AddHttpContextAccessor();
builder.Services.ConfigApiBehaviour();

var app = builder.Build();
app.UseMiddleware<CustomExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.UseSwaggerConfig();
// initialize data
await app.Initialize();

app.Run();