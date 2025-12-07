using DevLearning.CourseAPI.Repositories;
using DevLearning.CourseAPI.Services.Interfaces;
using Domain.Extensions;
using Domain.Models.DTOs.Course;
using Domain.Models.DTOs.Student;
using MongoDB.Bson;

namespace DevLearning.CourseAPI.Services;

public class CourseService(
    CourseRepository courseRepository,
    IHttpClientFactory httpClientFactory) : ICourseService
{
    private readonly CourseRepository _courseRepository = courseRepository;
    private readonly IHttpClientFactory _httpClientFactory = httpClientFactory;

    public async Task CreateCourseAsync(CourseRequestDTO course)
    {
        try
        {
            //var authorClient = _httpClientFactory.CreateClient("Author");
            //var categoryClient = _httpClientFactory.CreateClient("Category");

            //var verifyAuthor = await authorClient
            //    .GetFromJsonAsync<AuthorResponseDTO>(course.AuthorId.ToString());

            //var verifyCategory = await categoryClient
            //    .GetFromJsonAsync<CategoryResponseDTO>(course.CategoryId.ToString());

            //var verifyTitle = await _courseRepository
            //    .GetOneCourseByTitleAsync(course.Title);

            //if (verifyTitle is not null)
            //    throw new Exception("Título de curso já existente!");

            //if (verifyAuthor is null)
            //    throw new Exception("Autor inexistente!");

            //if (verifyCategory is null)
            //    throw new Exception("Categoria inexistente!");

            await _courseRepository.CreateCourseAsync(course.ToEntity());

        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CourseResponseDTO> DeleteCourseByTitleAsync(string title)
    {
        try
        {
            return await _courseRepository.DeleteCourseByTitleAsync(title);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<CourseResponseDTO>> GetAllCoursesAsync(string category)
    {
        try
        {
            return await _courseRepository.GetAllCoursesAsync(category);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CourseResponseDTO> GetOneCourseByTitleAsync(string title)
    {
        try
        {
            return await _courseRepository.GetOneCourseByTitleAsync(title);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateCourseByTitleAsync(string title, CourseUpdateDTO update)
    {
        try
        {
            await _courseRepository.UpdateCourseByTitleAsync(title, update.Free, update.Featured);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateActiveCourseByTitleAsync(string title)
    {
        try
        {
            var studentClient = _httpClientFactory.CreateClient("Student");

            var courseStorage = await _courseRepository.GetOneCourseByTitleAsync(title)
                ?? throw new Exception("Você não modificar um curso inexistente!");

            var studentCourse = await studentClient
                .GetFromJsonAsync<List<StudentResponseDTO>>(courseStorage.CourseId.ToString());

            if (studentCourse.Count > 0)
                throw new Exception("Você não pode inativar um curso com alunos nele!");

            await _courseRepository.UpdateActiveCourseByTitleAsync(title);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CourseResponseDTO> GetOneCourseByIdAsync(string id)
    {
        try
        {
            if (!ObjectId.TryParse(id, out var courseId))
                throw new ArgumentException("Incorrect id");

            return await _courseRepository.GetOneCourseByIdAsync(courseId);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
