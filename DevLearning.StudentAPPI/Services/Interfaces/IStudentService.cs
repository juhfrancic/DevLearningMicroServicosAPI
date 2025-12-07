using Domain.Models.DTOs.Course;
using Domain.Models.DTOs.Student;

namespace DevLearning.StudentAPI.Services.Interfaces
{
    public interface IStudentService
    {
        Task CreateStudent(StudentRequestDTO student);
        Task InsertStudentCourse(Guid studentId, Guid courseId, StudentRequestInsertCourseDTO studentCourse);
        Task<List<StudentResponseDTO>> GetAllStudents();
        Task<StudentResponseDTO> GetStudentByEmail(string email);
        Task<StudentResponseDTO> GetStudentByDocument(string document);
        Task<StudentResponseDTO> GetStudentById(string id);
        Task UpdateStudent(StudentRequestUpdateDTO student, string id);
        Task UpdateStudentCourse(Guid studentId, Guid courseId, StudentCourseRequestUpdateDTO studentCourse);
        Task<CourseStudentDTO> GetStudentCourse(string studentId, string courseId);
    }
}
