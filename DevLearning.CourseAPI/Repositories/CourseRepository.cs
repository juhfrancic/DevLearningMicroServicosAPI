using DevLearning.CourseAPI.Repositories.Interfaces;
using Domain.Extensions;
using Domain.Models;
using Domain.Models.DTOs.Course;
using Infrastructure.Data.Mongo.Contexts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DevLearning.CourseAPI.Repositories;

public class CourseRepository(
    MongoDbContext mongoDbContext
    ) : ICourseRepository
{
    private readonly IMongoCollection<Course> _courseCollection = mongoDbContext.Courses;
    public async Task CreateCourseAsync(Course course)
    {
        try
        {
            await _courseCollection.InsertOneAsync(course);
        }
        catch (MongoException mongoEx)
        {
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public Task<CourseResponseDTO> DeleteCourseByTitleAsync(string title)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CourseResponseDTO>> GetAllCoursesAsync(string category)
    {
        try
        {
            var courses = await _courseCollection
                .Find(course => course.CategoryName == category)
                .ToListAsync();

            return [.. courses.Select(c => c.ToDto())];
        }
        catch (MongoException mongoEx)
        {
            throw new MongoException(mongoEx.Message);
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
            var filter = Builders<Course>.Filter.Eq(c => c.Title, title);
            var course = await _courseCollection.Find(filter).FirstOrDefaultAsync();

            return course.ToDto();
        }
        catch (MongoException mongoEx)
        {
            throw new MongoException(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<CourseResponseDTO> GetOneCourseByIdAsync(ObjectId id)
    {
        try
        {
            var course = await _courseCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            return course.ToDto();
        }
        catch (MongoException mongoEx)
        {
            throw new Exception(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task UpdateCourseByTitleAsync(string title, bool free, bool featured)
    {
        try
        {
            var filter = Builders<Course>.Filter.Eq(c => c.Title, title);

            var update = Builders<Course>.Update
                .Set(c => c.Free, free)
                .Set(c => c.Featured, featured)
                .Set(c => c.LastUpdateDate, DateTime.UtcNow);

            await _courseCollection.UpdateOneAsync(filter, update);
        }
        catch (MongoException mongoEx)
        {
            throw new MongoException(mongoEx.Message);
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
            var course = await GetOneCourseByTitleAsync(title);
            var filter = Builders<Course>.Filter.Eq(c => c.Title, title);

            if (course.Active is true)
            {
                var update = Builders<Course>.Update
                    .Set(c => c.Active, false)
                    .Set(c => c.LastUpdateDate, DateTime.UtcNow);

                await _courseCollection.UpdateOneAsync(filter, update);
            }
            else
            {
                var update = Builders<Course>.Update
                    .Set(c => c.Active, true)
                    .Set(c => c.LastUpdateDate, DateTime.UtcNow);

                await _courseCollection.UpdateOneAsync(filter, update);
            }
        }
        catch (MongoException mongoEx)
        {
            throw new MongoException(mongoEx.Message);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
