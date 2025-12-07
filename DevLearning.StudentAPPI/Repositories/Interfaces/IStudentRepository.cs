using Domain.Models;
using Domain.Models.DTOs.Course;
using Domain.Models.DTOs.Student;
using MongoDB.Driver;

namespace DevLearning.StudentAPI.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task CreateStudent(Student student);
        Task UpdateStudent(Student student, Guid id);
        Task<List<StudentResponseDTO>> GetAllStudents();
        Task<StudentResponseDTO> GetStudentByDocument(string document);
        Task<StudentResponseDTO> GetStudentByEmail(string email);
        Task<StudentResponseDTO> GetStudentById(Guid id);
        Task<long> GetCountStudentCourse(Guid courseId);
        Task InsertStudentCourse(Guid studentId, Guid courseId, StudentRequestInsertCourseDTO studentCourse);
        Task UpdateStudentCourse(Guid studentId, Guid courseId, StudentCourseRequestUpdateDTO studentCourse);
        Task<CourseStudentDTO> GetStudentCourse(Guid studentId, Guid courseId);
    }
}