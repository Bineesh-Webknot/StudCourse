namespace StudCourseApp1.Dto;

public class GenericResponse<T>
{
    public String Status { get; set; }
    public int StatusCode { get; set; }
    public T Data { get; set; }
}