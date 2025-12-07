using Dapper;
using Domain.Models;
using Domain.Models.DTOs.CareerItem;
using Domain.Models.DTOs.Carrer;
using MongoDB.Bson;

namespace DevLearning.CareerAPI.Repositories.Interfaces
{
    public interface ICareerRepository
    {
        Task<IEnumerable<Career>> GetAllCareersAsync();
        Task<Career> GetCareerByIdAsync(ObjectId careerId);
        Task CreateCareerAsync(Career career);
        Task UpdateCareerAsync(Career career);
        Task UpdateActiveCareerAsync(ObjectId careerId);
        Task AddItemCareerAsync(CareerItem careerItem);
        Task<bool> RemoveItemCareerAsync(ObjectId careerId, ObjectId courseId);
        Task RemoveItemByCourseAsync(ObjectId careerId,ObjectId courseId);
        Task<List<ObjectId>> GetItemByCourseAsync(ObjectId courseId);
    }
}
