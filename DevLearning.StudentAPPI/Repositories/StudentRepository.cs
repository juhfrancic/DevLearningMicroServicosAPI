using DevLearning.StudentAPI.Repositories.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Course;
using Domain.Models.DTOs.Student;
using Infrastructure.Data.Mongo.Contexts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DevLearning.StudentAPI.Repositories;

public class StudentRepository : IStudentRepository
{
    private readonly IMongoCollection<Student> _students;
    private readonly IMongoCollection<StudentCourse> _studentCourses;
    //private readonly IMongoCollection<Course> _courses;
    public StudentRepository(MongoDbContext mongoClient)
    {
        _students = mongoClient.Students;
        _studentCourses = mongoClient.StudentCourses;
    }
    public async Task CreateStudent(Student student)
    {
        try
        {
            await _students.InsertOneAsync(student);
        }
        catch (MongoException mongoEx)
        {
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task InsertStudentCourse(ObjectId studentId, ObjectId courseId, StudentRequestInsertCourseDTO studentCourse)
    {
        try
        {
            var SC = new StudentCourse
            (
                courseId,
                studentId,
                studentCourse.Progress ?? 0,
                studentCourse.Favorite
            );


            await _studentCourses.InsertOneAsync(SC);
        }
        catch (MongoException mongoEx)
        {
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<List<StudentResponseDTO>> GetAllStudents()
    {
        try
        {
            var students = await _students.Find(_ => true).ToListAsync();


            return students.Select(s => new StudentResponseDTO
            {
                Id = s.Id.ToString(),
                Name = s.Name,
                Email = s.Email,
                Document = s.Document,
                Phone = s.Phone,
                BirthDate = s.BirthDate,
                CreateDate = s.CreateDate,
            }).ToList();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<Student> GetStudentByDocument(string document)
    {
        try
        {
            return await _students.Find(s => s.Document == document).FirstOrDefaultAsync();

        }
        catch (MongoException mongoEx)
        {
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<Student> GetStudentByEmail(string email)
    {
        try
        {
            return await _students.Find(s => s.Email == email).FirstOrDefaultAsync();
        }
        catch (MongoException mongoEx)
        {
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<Student> GetStudentById(ObjectId id)
    {
        try
        {
            return await _students.Find(s => s.Id == id).FirstOrDefaultAsync();
        }
        catch (MongoException mongoEx)
        {
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<Student> GetStudentByEmailAndDocument(string email, string document)
    {
        try
        {
            return await _students.Find(s => s.Email == email && s.Document == document).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<CourseStudentDTO> GetStudentCourse(ObjectId studentId, ObjectId courseId)
    {
        try
        {
            var student = await _students.Find(s => s.Id == studentId).FirstOrDefaultAsync();
            return null;
        }
        catch (MongoException mongoEx)
        {
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task UpdateStudent(Student student, ObjectId id)
    {
        try
        {
            var update = Builders<Student>.Update
                .Set(s => s.Name, student.Name)
                .Set(s => s.Email, student.Email)
                .Set(s => s.Document, student.Document)
                .Set(s => s.Phone, student.Phone)
                .Set(s => s.BirthDate, student.BirthDate);

            await _students.UpdateOneAsync(s => s.Id == id, update);
        }
        catch (MongoException mongoEx)
        {
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task UpdateStudentCourse(ObjectId studentId, ObjectId courseId, StudentCourseRequestUpdateDTO studentCourse)
    {
        try
        {
            var filter = Builders<StudentCourse>.Filter.Where(sc => sc.StudentId == studentId
                                                              && sc.CourseId == courseId);

            var update = Builders<StudentCourse>.Update
                .Set(sc => sc.Progress, studentCourse.Progress)
                .Set(sc => sc.Favorite, studentCourse.Favorite)
                .Set(sc => sc.LastUpdateDate, DateTime.UtcNow);

            await _studentCourses.UpdateOneAsync(filter, update);
        }
        catch (MongoException mongoEx)
        {
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<long> GetCountStudentCourse(ObjectId courseId)
    {
        try
        {
            var filter = Builders<StudentCourse>.Filter.Eq(sc => sc.CourseId, courseId);
            return await _studentCourses.CountDocumentsAsync(filter);
        }
        catch (MongoException mongoEx)
        {
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
