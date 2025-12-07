using Domain.Models.DTOs.Course;
using Domain.Models.DTOs.Student;
using MongoDB.Bson;

namespace DevLearning.StudentAPI.Services.Interfaces
{
    public interface IStudentService
    {
        Task CreateStudent(StudentRequestDTO student);
        Task InsertStudentCourse(string studentId, string courseId, StudentRequestInsertCourseDTO studentCourse);
        Task<List<StudentResponseDTO>> GetAllStudents();
        Task<StudentResponseDTO> GetStudentByEmail(string email);
        Task<StudentResponseDTO> GetStudentByDocument(string document);
        Task<StudentResponseDTO> GetStudentById(string id);
        Task UpdateStudent(StudentRequestUpdateDTO student, string id);
        Task UpdateStudentCourse(string studentId, string courseId, StudentCourseRequestUpdateDTO studentCourse);
        Task<CourseResponseDTO> GetStudentCourse(string studentId, string courseId);
    }
}
