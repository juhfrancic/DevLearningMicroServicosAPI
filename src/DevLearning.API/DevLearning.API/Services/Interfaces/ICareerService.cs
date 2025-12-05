using DevLearning.API.Models.DTOs.Carrer;

namespace DevLearning.API.Services.Interfaces
{
    public interface ICareerService
    {
        Task CreateCareerAsync(CareerRequestDTO careerDTO);
        Task<List<CareerWhitCareerItemResponseDTO>> GetAllCareerAsync();
        Task<CareerWhitCareerItemResponseDTO?> GetCareerByIdAsync(Guid careerId);
        Task<bool> DeleteCareerAsync(Guid careerId);
        Task<bool> UpdateCareerAsync(Guid id, CareerUpdateDTO updateDTO);
    }
}
