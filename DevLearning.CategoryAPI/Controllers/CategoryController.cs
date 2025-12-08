using DevLearning.CategoryAPI.Services.Interfaces;
using Domain.Models.DTOs.Category;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.CategoryAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<CategoryController> _logger;

    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> CreateCategory(CategoryRequestDTO categoryDto)
    {
        try
        {
            await _categoryService.CreateCategoryAsync(categoryDto);
            return Created();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Erro ao criar Categoria." + ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar Categoria." + ex.Message);
            return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<CategoryResponseDTO>>> GetAllCategories()
    {
        try
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar Categorias." + ex.Message);
            return StatusCode(500, $"Erro interno: {ex.Message}");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryResponseDTO>> GetById(Guid id)
    {
        try
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Erro ao buscar Categoria." + ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex, "Erro ao buscar Categoria." + ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar Categoria." + ex.Message);
            return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateCategory(Guid id, CategoryUpdateDTO categoryDto)
    {
        try
        {
            await _categoryService.UpdateCategoryAsync(id, categoryDto);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Erro ao atualizar Categoria." + ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex, "Erro ao atualizar Categoria." + ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao atualizar Categoria." + ex.Message);
            return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteCategory(Guid id)
    {
        try
        {
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Erro ao remover Categoria." + ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex, "Erro ao remover Categoria." + ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao remover Categoria." + ex.Message);
            return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
        }
    }

    [HttpGet("{id}/courses")]
    public async Task<IActionResult> GetCategoryCourses(Guid id)
    {
        try
        {
            var result = await _categoryService.GetCategoryCoursesAsync(id);

            if (result == null)
                return NotFound("Categoria não encontrada.");

            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            _logger.LogError(ex, "Erro ao buscar Categoria." + ex.Message);
            return BadRequest(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogError(ex, "Erro ao buscar Categoria." + ex.Message);
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar Categoria." + ex.Message);
            return StatusCode(500, new { message = $"Erro interno: {ex.Message}" });
        }
    }
}
