using DevLearning.StudentAPI.Services;
using Domain.Models.DTOs.Student;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentService _studentService;

        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost()]
        public async Task CreateStudent([FromBody] StudentRequestDTO student)
        {
            try
            {
                await _studentService.CreateStudent(student);
                Created();
            }
            catch (Exception ex)
            {
                StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost("/api/students/{studentId}/courses/{coursesId}")]
        public async Task<ActionResult> InsertStudentCourse([FromBody] StudentRequestInsertCourseDTO student, string studentId, string courseId)
        {
            try
            {
                await _studentService.InsertStudentCourse(studentId, courseId, student);
                return StatusCode(201, new { message = "Estudante adicionado com sucesso no curso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("{studentId}")]
        public async Task<ActionResult> UpdateStudent([FromBody] StudentRequestUpdateDTO student, string studentId)
        {
            try
            {
                await _studentService.UpdateStudent(student, studentId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<StudentResponseDTO>>> GetAllStudents()
        {
            try
            {
                var students = await _studentService.GetAllStudents();
                if (students is null && students.Count == 0)
                    return NotFound();
                else
                    return Ok(students);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetStudentById(string id)
        {
            try
            {
                var student = await _studentService.GetStudentById(id);
                if (student is null)
                    return NotFound();
                else
                    return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("Document/{document}")]
        public async Task<ActionResult> GetStudentByDocument(string document)
        {
            try
            {
                var studentStorage = await _studentService.GetStudentByDocument(document);
                if (studentStorage is null)
                    return NotFound();
                else
                    return Ok(studentStorage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet("Email/{email}")]
        public async Task<ActionResult> GetStudentByEmail(string email)
        {
            try
            {
                var studentStorage = await _studentService.GetStudentByEmail(email);
                if (studentStorage is null)
                    return NotFound();
                else
                    return Ok(studentStorage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPut("students/{studentId}/courses/{courseId}")]
        public async Task<ActionResult> UpdateStudentCourse([FromBody] StudentCourseRequestUpdateDTO student, string studentId, string courseId)
        {
            try
            {
                await _studentService.UpdateStudentCourse(studentId, courseId, student);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}
