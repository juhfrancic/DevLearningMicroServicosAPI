using Domain.Models;
using Domain.Models.DTOs.Course;

namespace DevLearning.CourseAPI.Repositories.Interfaces
{
    public interface ICourseRepository
    {
        Task CreateCourseAsync(Course course);

        Task<List<CourseResponseDTO>> GetAllCoursesAsync(string category);

        Task<CourseResponseDTO> GetOneCourseByIdAsync(Guid id);
        Task<CourseResponseDTO> GetOneCourseByTitleAsync(string title);

        Task<CourseResponseDTO> DeleteCourseByTitleAsync(string title);

        Task UpdateCourseByTitleAsync(string title, bool free, bool featured);

        Task UpdateActiveCourseByTitleAsync(string title);
    }
}
