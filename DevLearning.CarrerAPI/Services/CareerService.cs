using DevLearning.CareerAPI.Repositories.Interfaces;
using DevLearning.CareerAPI.Services.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.CareerItem;
using Domain.Models.DTOs.Carrer;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DevLearning.CareerAPI.Services;

public class CareerService : ICareerService
{
    private readonly ICareerRepository _careerRepository;

   // private readonly IHttpClientFactory _httpClientFactory;

    public CareerService(ICareerRepository careerRepository/*, IHttpClientFactory httpClientFactory*/)
    {
        _careerRepository = careerRepository;
       // _httpClientFactory = httpClientFactory;
    }

    //aqui
    public async Task AddItemCareerAsync(string careerId, CareerItemRequestDTO careerItemDTO)
    {
        //var client = _httpClientFactory.CreateClient("Course");

        if (!ObjectId.TryParse(careerId, out ObjectId careerObjectId))
            throw new ArgumentException("The ID is not in ObjectId format", nameof(careerId));

        if (!ObjectId.TryParse(careerItemDTO.CourseId, out ObjectId courseObjectId))
            throw new ArgumentException("The ID is not in ObjectId format", nameof(careerItemDTO.CourseId));

        //var course = client.GetFromJsonAsync<Course>($"api/Course/get-by-title/{careerItemDTO.Title}") 
        //    ?? throw new Exception("Register not found!");

        if (careerItemDTO.Order <= 0)
            throw new ArgumentException("Order must be greater than 0!");

        try
        {
            var existing = await _careerRepository.GetCareerByIdAsync(careerObjectId);
            if (existing is null)
                throw new KeyNotFoundException("Career not found!");

            var item = new CareerItem(careerObjectId, courseObjectId, careerItemDTO.Title, careerItemDTO.Description, careerItemDTO.Order);

            await _careerRepository.AddItemCareerAsync(item);
        }
        catch (KeyNotFoundException) { throw; }
        catch (ArgumentException) { throw; }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task CreateCareerAsync(CareerRequestDTO careerDTO)
    {
        if (string.IsNullOrEmpty(careerDTO.Title))
            throw new ArgumentException("This title is required!");

        var career = new Career(careerDTO.Title, careerDTO.Summary, careerDTO.Url, careerDTO.Tags, careerDTO.Featured);

        try
        {
            await _careerRepository.CreateCareerAsync(career);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<CareerResponseDto>> GetAllCareersAsync()
    {
        try
        {
            var careers = (await _careerRepository.GetAllCareersAsync()).ToList();

            var dtos = careers.Select(c => new CareerResponseDto
            {
                Id = c.Id.ToString(),
                Title = c.Title,
                Summary = c.Summary,
                Url = c.Url,
                DurationInMinutes = c.DurationInMinutes,
                Active = c.Active,
                Featured = c.Featured,
                Tags = c.Tags,
                Items = c.Items.Select(i => new CareerItemResponseDto
                {
                    CourseId = i.CourseId.ToString(),
                    Title = i.Title,
                    Description = i.Description,
                    Order = i.Order
                }).ToList()
            }).ToList();

            return dtos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CareerResponseDto> GetCareerByIdAsync(string careerId)
    {
        if (!ObjectId.TryParse(careerId, out ObjectId objectId))
            throw new ArgumentException("The ID is not in ObjectId format", nameof(careerId));
        
        try
        {
            var career = await _careerRepository.GetCareerByIdAsync(objectId);

            if (career is null)
                throw new KeyNotFoundException("Career not found!");

            var dto = new CareerResponseDto
            {
                Id = career.Id.ToString(),
                Title = career.Title,
                Summary = career.Summary,
                Url = career.Url,
                DurationInMinutes = career.DurationInMinutes,
                Active = career.Active,
                Featured = career.Featured,
                Tags = career.Tags,
                Items = career.Items.Select(i => new CareerItemResponseDto
                {
                    CourseId = i.CourseId.ToString(),
                    Title = i.Title,
                    Description = i.Description,
                    Order = i.Order
                }).ToList()
            };

            return dto;
        }
        catch (KeyNotFoundException) { throw; }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task RemoveItemCareerAsync(string careerId, string courseId)
    {

        if (!ObjectId.TryParse(careerId, out ObjectId careerObjectId))
            throw new ArgumentException("The Career ID is not in ObjectId format", nameof(careerId));

        if (!ObjectId.TryParse(courseId, out ObjectId courseObjectId))
            throw new ArgumentException("The Course ID is not in ObjectId format", nameof(courseId));

        try
        {
            var existingCareer = await _careerRepository.GetCareerByIdAsync(careerObjectId);
            if (existingCareer is null)
                throw new KeyNotFoundException("Career not found!");

            var removed = await _careerRepository.RemoveItemCareerAsync(careerObjectId, courseObjectId);

            if (!removed)
                throw new KeyNotFoundException("This course doesn't belong to this career.");
        }
        catch (KeyNotFoundException) { throw; }
        catch (ArgumentException) { throw; }
        catch (Exception ex)
        {

            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateActiveCareerAsync(string careerId)
    {

        if (!ObjectId.TryParse(careerId, out ObjectId objectId))
            throw new ArgumentException("The ID is not in ObjectId format", nameof(careerId));

        try
        {
            var existing = await _careerRepository.GetCareerByIdAsync(objectId);
            if (existing is null)
                throw new KeyNotFoundException("Career not found!");

            await _careerRepository.UpdateActiveCareerAsync(objectId);
        }
        catch (KeyNotFoundException) { throw; }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateCareerAsync(string careerId, CareerUpdateDTO careerDTO)
    {

        if (!ObjectId.TryParse(careerId, out ObjectId objectId))
            throw new ArgumentException("The ID is not in ObjectId format", nameof(careerId));

        try
        {
            var existing = await _careerRepository.GetCareerByIdAsync(objectId);
            if (existing is null)
                throw new KeyNotFoundException("Career not found!");


            var updated = new Career(objectId, careerDTO.Title, careerDTO.Summary, careerDTO.Url,
                                     existing.DurationInMinutes, careerDTO.Active, careerDTO.Featured, careerDTO.Tags);

            await _careerRepository.UpdateCareerAsync(updated);
        }
        catch (KeyNotFoundException) { throw; }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    //aqui
    public async Task RemoveItemByCourseAsync(string courseId)
    {

        if (!ObjectId.TryParse(courseId, out ObjectId objectId))
            throw new ArgumentException("The ID is not in ObjectId format", nameof(courseId));

        var careerIds = await _careerRepository.GetItemByCourseAsync(objectId);

        foreach (var careerId in careerIds)
        {
            await _careerRepository.RemoveItemByCourseAsync(careerId, objectId);
        }
    }
}
