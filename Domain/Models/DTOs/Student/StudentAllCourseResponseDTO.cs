namespace Domain.Models.DTOs.Student;

public class StudentAllCourseResponseDTO
{
    public string Name { get; init; }
    public string Email { get; init; }
    public List<CoursePerStudentDTO> Courses { get; set; } = new();
}
