using DevLearning.StudentAPI.Repositories;
using DevLearning.StudentAPI.Services.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Domain.Models.DTOs.Course;
using Domain.Models.DTOs.Student;
using MongoDB.Bson;
using MongoDB.Driver;


namespace DevLearning.StudentAPI.Services;

public class StudentService : IStudentService
{
    private readonly StudentRepository _studentRepository;
    private readonly IHttpClientFactory _httpClientFactory;

    public StudentService(StudentRepository studentRepository, IHttpClientFactory httpClientFactory)
    {
        _studentRepository = studentRepository;
        _httpClientFactory = httpClientFactory;
    }
    public async Task CreateStudent(StudentRequestDTO student)
    {
        try
        {
            var studentStorage = await _studentRepository.GetStudentByEmailAndDocument(student.Email, student.Document);
            if (studentStorage is not null)
                throw new Exception("Estudante com email ou documento já cadastrado.");

            var newStudent = new Student(
                student.Name,
                student.Email,
                student.Document,
                student.Phone,
                student.Birthdate
            );
            await _studentRepository.CreateStudent(newStudent);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task InsertStudentCourse(string studentId, string courseId, StudentRequestInsertCourseDTO studentCourse)
    {
        try
        {
            if (await _studentRepository.GetStudentById(ObjectId.Parse(studentId)) is null)
                throw new Exception("Estudante não encontrado");

            if (await _studentRepository.GetStudentCourse(ObjectId.Parse(studentId), ObjectId.Parse(courseId)) is not null)
                throw new Exception("Estudante já está matriculado nesse curso");

            await _studentRepository.InsertStudentCourse(ObjectId.Parse(studentId), ObjectId.Parse(courseId), studentCourse);
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
            return await _studentRepository.GetAllStudents();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<StudentResponseDTO> GetStudentByEmail(string email)
    {
        try
        {
            var student = await _studentRepository.GetStudentByEmail(email)
                ?? throw new Exception("Estudante não encontrado");

            return new StudentResponseDTO
            {
                Id = student.Id.ToString(),
                Name = student.Name,
                Email = student.Email,
                Document = student.Document,
                Phone = student.Phone,
                BirthDate = student.BirthDate,
                CreateDate = student.CreateDate
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<StudentResponseDTO> GetStudentByDocument(string document)
    {
        try
        {
            var student = await _studentRepository.GetStudentByDocument(document);

            if (student is null)
                throw new Exception("Estudante não encontrado");

            return new StudentResponseDTO
            {
                Id = student.Id.ToString(),
                Name = student.Name,
                Email = student.Email,
                Document = student.Document,
                Phone = student.Phone,
                BirthDate = student.BirthDate,
                CreateDate = student.CreateDate
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<StudentResponseDTO> GetStudentById(string id)
    {
        try
        {
            var student = await _studentRepository.GetStudentById(ObjectId.Parse(id));

            if (student is null)
                throw new Exception("Estudante não encontrado");

            return new StudentResponseDTO
            {
                Id = student.Id.ToString(),
                Name = student.Name,
                Email = student.Email,
                Document = student.Document,
                Phone = student.Phone,
                BirthDate = student.BirthDate,
                CreateDate = student.CreateDate
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task UpdateStudent(StudentRequestUpdateDTO student, string id)
    {
        try
        {
            var studentStorage = await _studentRepository.GetStudentById(ObjectId.Parse(id));
            if (studentStorage is null)
                throw new Exception("Estudante não encontrado");
            if (await _studentRepository.GetStudentByDocument(student.Document) is not null)
                throw new Exception("O documento informado já está cadastrado.");
            if (student.Email is not null && await _studentRepository.GetStudentByEmail(student.Email) is not null)
                throw new Exception("O email informado já está cadastrado.");

            var newStudent = new Student(
                student.Name ?? studentStorage.Name,
                student.Email ?? studentStorage.Email,
                student.Document ?? studentStorage.Document,
                student.Phone ?? studentStorage.Phone,
                student.Birthdate ?? studentStorage.BirthDate.Value
                );
            await _studentRepository.UpdateStudent(newStudent, ObjectId.Parse(id));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task UpdateStudentCourse(string studentId, string courseId, StudentCourseRequestUpdateDTO scDto)
    {
        try
        {
            var student = await _studentRepository.GetStudentById(ObjectId.Parse(studentId));
            if (student is null)
                throw new Exception("Estudante não encontrado");

            var studentCourse = await _studentRepository.GetStudentCourse(ObjectId.Parse(studentId), ObjectId.Parse(courseId));
            if (studentCourse is null)
                throw new Exception("Curso não encontrado para o estudante");

            if (scDto.Progress < 0)
                throw new Exception("Progresso não pode ser menor que 0");

            if (scDto.Progress > 100)
                throw new Exception("Progresso não pode ser maior que 100");


            await _studentRepository.UpdateStudentCourse(ObjectId.Parse(studentId), ObjectId.Parse(courseId), scDto);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
    public async Task<CourseResponseDTO> GetStudentCourse(string studentId, string courseId)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("Course");

            var allStudents = await _studentRepository.GetAllStudents();

            var student = allStudents.FirstOrDefault(s => s.Id == studentId);

            if (student is null)
                return null;

            var course = await client.GetFromJsonAsync<Course>($"api/course/id/{courseId}");

            //var course1 = student.Courses.FirstOrDefault(c => c.CourseId == courseId);

            return course.ToDto();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}