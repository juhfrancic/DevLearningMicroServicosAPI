using Dapper;
using DevLearning.CategoryAPI.Repositories.Interfaces;
using Domain.Models;
using Domain.Models.DTOs.Category;
using Infrastructure.Data.SQL.Contexts;
using Microsoft.Data.SqlClient;

namespace DevLearning.CategoryAPI.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly SqlConnection _connection;

    public CategoryRepository(ConnectionDBCategory connection)
    {
        _connection = connection.GetConnection();
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
            throw;
        }
    }

    public async Task<string> GetCategoryCoursesAsync(Guid categoryId)
    {
        try
        {
            var sql = @"SELECT cat.Title AS CategoryTitle
                        FROM Category cat
                        WHERE cat.Id = @CategoryId";

            var rows = await _connection.QueryFirstOrDefaultAsync(sql, new { CategoryId = categoryId });
            return rows;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}

