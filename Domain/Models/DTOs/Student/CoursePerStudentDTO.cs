namespace Domain.Models.DTOs.Student;

public class CoursePerStudentDTO
{
    public string Title { get; init; }
    public byte Level { get; init; }
    public int DurationInMinutes { get; init; }
    public bool Active { get; init; }
    public int Progress { get; init; }
}
