
using DevLearning.CareerAPI.Repositories.Interfaces;
using Domain.Models;
using Infrastructure.Data.Mongo.Contexts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DevLearning.CareerAPI.Repositories
{
    public class CareerRepository : ICareerRepository
    {

        private readonly IMongoCollection<Career> _careersCollection;

        private readonly IMongoCollection<CareerItem> _careerItemsCollection;

        public CareerRepository(MongoDbContext mongoDbContext)
        {
            _careersCollection = mongoDbContext.Careers;
            _careerItemsCollection = mongoDbContext.CareerItems;
        }

        public async Task AddItemCareerAsync(CareerItem careerItem)
        {
            try
            {
                await _careerItemsCollection.InsertOneAsync(careerItem);

                var filter = Builders<Career>.Filter.Eq(career => career.Id, careerItem.CareerId);

                var update = Builders<Career>.Update.Push(c => c.Items, careerItem);
                await _careersCollection.UpdateOneAsync(filter, update);

                //ARRUMAR OS MINUTOS var durationInMinutesCourse = 
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

        public async Task CreateCareerAsync(Career career)
        {
            try
            {
                await _careersCollection.InsertOneAsync(career);

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

        public async Task<IEnumerable<Career>> GetAllCareersAsync()
        {
            try
            {
                var filter = Builders<Career>.Filter.Empty;

                return await (await _careersCollection.FindAsync(filter)).ToListAsync();

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

        public async Task<Career> GetCareerByIdAsync(ObjectId careerId)
        {
            try
            {
                var filter = Builders<Career>.Filter.Eq(c => c.Id, careerId);
                return await _careersCollection.Find(filter).FirstOrDefaultAsync();

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

        public async Task RemoveItemByCourseAsync(ObjectId careerId, ObjectId courseId)
        {
            try
            {
                var mainFilter = Builders<Career>.Filter.ElemMatch(
                                                                   c => c.Items, 
                                                                   Builders<CareerItem>.Filter.Eq(item => item.CourseId, courseId)
                                                                   );
                var pullFilter = Builders<CareerItem>.Filter.Eq(item => item.CourseId, courseId);

                var update = Builders<Career>.Update.PullFilter(c => c.Items, pullFilter);

                var result = await _careersCollection.UpdateManyAsync(mainFilter, update);

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

        public async Task<bool> RemoveItemCareerAsync(ObjectId careerId, ObjectId courseId)
        {
            try
            {
                var filter = Builders<CareerItem>.Filter.And(
                    Builders<CareerItem>.Filter.Eq(item => item.CareerId, careerId),
                    Builders<CareerItem>.Filter.Eq(item => item.CourseId, courseId));

                var result = await _careerItemsCollection.DeleteOneAsync(filter);

                if (result.DeletedCount is 0)
                    throw new KeyNotFoundException($"Não há ligação entre {careerId} e {courseId}.");

                var filterUpdate = Builders<Career>.Filter.Eq(career => career.Id, careerId);

                var pullFilter = Builders<CareerItem>.Filter.Eq(item => item.CourseId, courseId);

                var update = Builders<Career>.Update.PullFilter(c => c.Items, pullFilter);

                var resultUpdate = await _careersCollection.UpdateOneAsync(filterUpdate, update);

                //ARRUMAR MINUTOS  var filterUpdate = 
                return true;

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

        public async Task UpdateActiveCareerAsync(ObjectId careerId)
        {
            try
            {
                var filter = Builders<Career>.Filter.Eq(c => c.Id, careerId);

                var update = Builders<Career>.Update.Set(c => c.Active, false);

                var result = await _careersCollection.UpdateOneAsync(filter, update);

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

        public async Task UpdateCareerAsync(Career career)
        {
            try
            {
                var filter = Builders<Career>.Filter.Eq(c => c.Id, career.Id);

                await _careersCollection.ReplaceOneAsync(filter, career);
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

        public async Task<List<ObjectId>> GetItemByCourseAsync(ObjectId courseId)
        {

            try
            {
                var filter = Builders<CareerItem>.Filter.Eq(item => item.CourseId, courseId);
                var projection = Builders<CareerItem>.Projection.Include(item => item.CareerId);

                return await _careerItemsCollection.Find(filter).Project(item => item.CareerId).ToListAsync();

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
}
