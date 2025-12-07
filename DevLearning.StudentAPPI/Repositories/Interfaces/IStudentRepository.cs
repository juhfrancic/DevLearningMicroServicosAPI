using Domain.Models;
using Domain.Models.DTOs.Course;
using Domain.Models.DTOs.Student;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DevLearning.StudentAPI.Repositories.Interfaces
{
    public interface IStudentRepository
    {
        Task CreateStudent(Student student);
        Task InsertStudentCourse(ObjectId studentId, ObjectId courseId, StudentRequestInsertCourseDTO studentCourse);
        Task<List<StudentResponseDTO>> GetAllStudents();
        Task<Student> GetStudentByDocument(string document);
        Task<Student> GetStudentByEmail(string email);
        Task<Student> GetStudentById(ObjectId id);
        Task<Student> GetStudentByEmailAndDocument(string email, string document);
        Task<CourseStudentDTO> GetStudentCourse(ObjectId studentId, ObjectId courseId);
    }
}