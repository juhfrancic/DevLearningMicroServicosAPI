using Dapper;
using DevLearning.CategoryAPI.Data;
using DevLearning.CategoryAPI.Repositories.Interfaces;
using DevLearning.CourseAPI.Repositories;
using Domain.Models;
using Domain.Models.DTOs.Category;
using Infrastructure.Data;
using Microsoft.Data.SqlClient;
using System.Data.Common;

namespace DevLearning.CategoryAPI.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SqlConnection _connection;
        private readonly ILogger<CategoryRepository> _logger;

        private CourseRepository _courseRepository;
        public CategoryRepository(ConnectionDBCategory connection, CourseRepository courseRepository, ILogger<CategoryRepository> logger)
        {
            _connection = connection.GetConnection();
            _courseRepository = courseRepository;
            _logger = logger;
        }

        public async Task<bool> CategoryTitleExistsAsync(string title)
        {
            try
            {
                var sql = "SELECT COUNT(*) FROM Category WHERE Title = @Title";
                var count = await _connection.ExecuteScalarAsync<int>(sql, new { Title = title });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> CategoryTitleExistsForOtherIdAsync(string title, Guid id)
        {
            try
            {
                var sql = "SELECT COUNT(*) FROM Category WHERE Title = @Title AND Id <> @Id";
                var count = await _connection.ExecuteScalarAsync<int>(sql, new { Title = title, Id = id });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task ShiftOrdersAsync(int order)
        {
            try
            {
                var sql = @"UPDATE Category
                SET [Order] = [Order] + 1
                WHERE [Order] >= @Order";

                await _connection.ExecuteAsync(sql, new { Order = order });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task ShiftOrdersForUpdateAsync(int oldOrder, int newOrder, Guid categoryId)
        {
            try
            {
                string sql;

                if (newOrder < oldOrder)
                {
                    sql = @"UPDATE Category
                SET [Order] = [Order] + 1
                WHERE [Order] >= @NewOrder AND [Order] < @OldOrder
                AND Id <> @CategoryId";
                }
                else
                {
                    sql = @"UPDATE Category
                SET [Order] = [Order] - 1
                WHERE [Order] > @OldOrder AND [Order] <= @NewOrder
                AND Id <> @CategoryId";
                }

                await _connection.ExecuteAsync(sql, new { NewOrder = newOrder, OldOrder = oldOrder, CategoryId = categoryId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task CreateCategoryAsync(Category category)
        {
            try
            {
                var sql = @"INSERT INTO Category (Id, Title, Url, Summary, [Order], Description, Featured) 
                      VALUES (@Id, @Title, @Url, @Summary, @Order, @Description, @Featured)";

                await _connection.ExecuteAsync(sql, new
                {
                    category.Id,
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task<List<CategoryResponseDTO>> GetAllCategoriesAsync()
        {
            try
            {
                var sql = @"SELECT Id, Title, Url, Summary, [Order], Description, Featured
                        FROM Category
                        ORDER BY [Order]";

                var categories = await _connection.QueryAsync<CategoryResponseDTO>(sql);
                return categories.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<Category> GetCategoryByIdAsync(Guid id)
        {
            try
            {
                var sql = @"SELECT Id, Title, Url, Summary, [Order], Description, Featured
                        FROM Category
                        WHERE Id = @Id";

                var category = await _connection.QueryFirstOrDefaultAsync<Category>(sql, new { Id = id });
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            try
            {
                var sql = @"UPDATE Category
                        SET Title = @Title,
                        Url = @Url,
                        Summary = @Summary,
                        [Order] = @Order,
                        Description = @Description,
                        Featured = @Featured
                        WHERE Id = @Id";

                await _connection.ExecuteAsync(sql, new
                {
                    category.Title,
                    category.Url,
                    category.Summary,
                    category.Order,
                    category.Description,
                    category.Featured,
                    category.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<bool> HasCourseAsync(Guid categoryId)
        {
            try
            {
                var sql = "SELECT COUNT(*) FROM Course WHERE CategoryId = @Id";
                var count = await _connection.ExecuteScalarAsync<int>(sql, new { Id = categoryId });
                return count > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public async Task DeleteCategoryAsync(Guid id)
        {
            try
            {
                var deleteCategorySql = "DELETE FROM Category WHERE Id = @Id";
                await _connection.ExecuteAsync(deleteCategorySql, new { Id = id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }

        public async Task<(string CategoryTitle, List<string> Courses)> GetCategoryCoursesAsync(Guid categoryId)
        {
            try
            {
                var sql = @"SELECT cat.Title AS CategoryTitle, c.Title AS CourseTitle
                        FROM Category cat
                        LEFT JOIN Course c 
                        ON cat.Id = c.CategoryId
                        WHERE cat.Id = @CategoryId";

                var rows = await _connection.QueryAsync(sql, new { CategoryId = categoryId });

                if (!rows.Any())
                    return (null, new List<string>());

                string categoryTitle = rows.First().CategoryTitle;
                var courses = rows.Select(r => (string)r.CourseTitle)
                                  .Where(c => c != null)
                                  .ToList();

                return (categoryTitle, courses);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
