using Domain.Models;
using Domain.Models.DTOs.CareerItem;
using Domain.Models.DTOs.Carrer;
using MongoDB.Bson;

namespace DevLearning.CareerAPI.Services.Interfaces
{
    public interface ICareerService
    {
        Task<List<CareerResponseDto>> GetAllCareersAsync();
        Task<CareerResponseDto> GetCareerByIdAsync(string careerId);
        Task CreateCareerAsync(CareerRequestDTO careerDTO);
        Task UpdateCareerAsync(string careerId, CareerUpdateDTO careerDTO);
        Task UpdateActiveCareerAsync(string careerId);
        Task AddItemCareerAsync(string careerId, CareerItemRequestDTO careerItemDTO);
        Task RemoveItemCareerAsync(string careerId, string courseId);
    }
}
