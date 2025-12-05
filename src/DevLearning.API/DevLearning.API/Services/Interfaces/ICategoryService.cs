using DevLearning.API.Models.DTOs.Category;

namespace DevLearning.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task CreateCategoryAsync(CategoryRequestDTO categoryDto);
        Task<List<CategoryResponseDTO>> GetAllCategoriesAsync();
        Task<CategoryResponseDTO> GetCategoryByIdAsync(Guid id);
        Task UpdateCategoryAsync(Guid id, CategoryUpdateDTO categoryDto);
        Task DeleteCategoryAsync(Guid id);
        Task<CategoryWithCoursesDTO> GetCategoryCoursesAsync(Guid categoryId);
    }
}
