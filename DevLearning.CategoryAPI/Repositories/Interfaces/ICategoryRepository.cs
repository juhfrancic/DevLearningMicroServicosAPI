using Domain.Models;
using Domain.Models.DTOs.Category;
using Domain.Models.DTOs.Course;

namespace DevLearning.CategoryAPI.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<bool> CategoryTitleExistsAsync(string title);
        Task<bool> CategoryTitleExistsForOtherIdAsync(string title, Guid id);
        Task ShiftOrdersAsync(int order);
        Task ShiftOrdersForUpdateAsync(int oldOrder, int newOrder, Guid categoryId);
        Task CreateCategoryAsync(Category category);
        Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(Guid id);
        Task UpdateCategoryAsync(Category category);
        Task<bool> HasCourseAsync(Guid categoryId);
        Task DeleteCategoryAsync(Guid id);
        Task<string> GetCategoryCoursesAsync(Guid categoryId);
    }
}
