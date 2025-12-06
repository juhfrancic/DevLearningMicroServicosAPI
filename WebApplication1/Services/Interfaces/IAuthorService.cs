using Domain.Models.DTOs.Author;
using Domain.Models.Enums.Author;

namespace DevLearning.AuthorAPI.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<List<AuthorResponseDTO>> GetAllAuthorsAsync();
        Task<AuthorResponseDTO> GetAuthorByIdAsync(Guid id);
        Task CreateAuthorAsync(AuthorRequestDTO author);
        Task UpdatePatchAuthorAsync(Guid id, UpdateAuthorParcialDTO dto);
        Task UpdatePutAuthorAsync(Guid id, UpdateAuthorFullDTO dto);
        Task UpdateAuthorTypeAsync(Guid id, AuthorType type);
        Task<AuthorWithCoursesDTO> GetAuthorCoursesAsync(Guid authorId);

    }
}
