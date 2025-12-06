using Dapper;
using DevLearning.StudentAPI.Repositories.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Course;
using Domain.Models.DTOs.Student;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using MongoDB.Driver;
using System;
using System.Data.Common;
using System.Numerics;
using System.Reflection.Metadata;
using System.Xml.Linq;

namespace DevLearning.StudentAPI.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly IMongoCollection<Student> _students;
        private readonly IMongoCollection<StudentCourse> _studentCourses;
        private readonly IMongoCollection<Course> _courses;
        public StudentRepository(IMongoDatabase database)
        {
            _students = database.GetCollection<Student>("Students");
            _studentCourses = database.GetCollection<StudentCourse>("StudentCourses");
        }
        public async Task CreateStudent(Student student)
        {
            try
            {
                
                await _students.InsertOneAsync(student);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task InsertStudentCourse(Guid studentId, Guid courseId, StudentRequestInsertCourseDTO studentCourse)
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
                var studentCourse = await _studentCourses.Find(_ => true).ToListAsync();
                var courses = await _courses.Find(_ => true).ToListAsync();

                var coursesDictionary = courses.ToDictionary(cd => cd.Id, cd => cd);
                var studentCourseDictionary = studentCourse.GroupBy(s => s.StudentId).ToDictionary(g => g.Key, g => g.ToList());



                return students.Select(s => new StudentResponseDTO
                {
                    StudentId = s.Id,
                    Name = s.Name,
                    Email = s.Email,
                    Document = s.Document,
                    Phone = s.Phone,
                    Birthdate = s.Birthdate,
                    CreateDate = s.CreateDate,
                    Courses = studentCourseDictionary.ContainsKey(s.Id) ? studentCourseDictionary[s.Id].Select
                    (sc =>
                    {

                        if (!coursesDictionary.TryGetValue(sc.CourseId, out var course))
                        {

                            return new CourseStudentDTO
                            {
                                CourseId = sc.CourseId,
                                Title = null,
                                Summary = null,
                                Url = null,
                                Level = default,
                                Progress = (byte)sc.Progress,
                                DurationInMinutes = 0

                            };
                        }

                        return new CourseStudentDTO
                        {
                            CourseId = sc.CourseId,
                            Title = course.Title,
                            Summary = course.Summary,
                            Url = course.Url,
                            Level = course.Level,
                            Progress = (byte)sc.Progress,
                            DurationInMinutes = 0
                        };
                    }).ToList() : new List<CourseStudentDTO>()
                }).ToList();               
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
                var allStudents = await GetAllStudents();
                return allStudents.FirstOrDefault(s => s.Document == document);
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
                var allStudents = await GetAllStudents();
                return allStudents.FirstOrDefault(s => s.Email == email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<StudentResponseDTO> GetStudentById(Guid id)
        {
            try
            {
                var allStudents = await GetAllStudents();
                return allStudents.FirstOrDefault(s => s.StudentId == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<StudentResponseDTO> GetStudentByEmailAndDocument(string email, string document)
        {
            try
            {
                var allStudents = await GetAllStudents();
                return allStudents.FirstOrDefault(s => s.Email == email && s.Document == document);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StudentCourseResponseDTO> GetStudentCourse(Guid studentId, Guid courseId)
        {
            try
            {
                var sql = @"SELECT 
                            s.Id AS StudentId, 
                            s.Name AS [Name], 
                            s.Email AS Email, 
                            s.Document AS Document, 
                            s.Phone AS Phone, 
                            s.Birthdate AS BirthDate, 
                            s.CreateDate AS CreateDate,
                            c.Id AS CourseId, 
                            c.Title AS Title, 
                            c.Summary AS Summary, 
                            c.Url AS [Url], 
                            c.Level AS [Level], 
                            c.DurationInMinutes AS DurationInMinutes,
                            sc.Progress AS Progress, 
                            sc.Favorite AS Favorite, 
                            sc.StartDate AS StartDate, 
                            sc.LastUpdateDate AS LastUpdateDate
                        FROM StudentCourse sc
                        INNER JOIN Student s ON sc.StudentId = s.Id
                        INNER JOIN Course c ON sc.CourseId = c.Id
                        WHERE sc.StudentId = @StudentId
                          AND sc.CourseId = @CourseId;";
                var studentCourse = await _connection.QueryAsync<
                    StudentResponseDTO,
                    CourseStudentDTO,
                    StudentCourseResponseDTO,
                    StudentCourseResponseDTO>(sql, (student, course, studentCourse) =>
                {
                    studentCourse.Student = student;
                    studentCourse.Course = course;
                    return studentCourse;
                },
                param: new { StudentId = studentId, CourseId = courseId },
                splitOn: "CourseId, Progress"
                );

                return studentCourse.FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateStudent(Student student, Guid id)
        {
            try
            {
                var update = Builders<Student>.Update
                    .Set(s => s.Name, student.Name)
                    .Set(s => s.Email, student.Email)
                    .Set(s => s.Document, student.Document)
                    .Set(s => s.Phone, student.Phone)
                    .Set(s => s.Birthdate, student.Birthdate);

                await _students.UpdateOneAsync(s => s.Id == id, update);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateStudentCourse(Guid studentId, Guid courseId, StudentCourseRequestUpdateDTO studentCourse)
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Task<int> GetCountStudentCourse(Guid courseId)
        {
            try
            {
                var sql = @"SELECT COUNT(*) FROM StudentCourse WHERE CourseId = @CourseId";
                return _connection.ExecuteScalarAsync<int>(sql, new { CourseId = courseId });
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
