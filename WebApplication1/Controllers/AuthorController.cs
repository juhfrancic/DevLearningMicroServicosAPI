using DevLearning.API.Models;
using DevLearning.API.Services.Interfaces;
using DevLearning.AuthorAPI.Services;
using Domain.Models.DTOs.Author;
using Domain.Models.Enums.Author;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.AuthorAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthorController(
    AuthorService authorService
) : ControllerBase
{

    //Listar todos os autores
    [HttpGet]
    public async Task<ActionResult<List<AuthorResponseDTO>>> GetAllAuthors()
    {
        try
        {
            var authors = await authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }
        catch (Exception ex)
        {
            return StatusCode(
                404,
                new 
                {
                    error = $"Lista de autores não encontrada. {ex.Message}" 
                }
            );
        }
    }

    //Listar autor por Id
    [HttpGet("{id}")]
    public async Task<ActionResult> GetAuthorById(Guid id)
    {
        try
        {
            var author = await authorService.GetAuthorByIdAsync(id);
            if (author is null)
                return StatusCode(404, new { message = "Autor não encontrado" });
            else
                return Ok(author);
        }
        catch (Exception ex)
        {
            return StatusCode(404, new { error = $"Autor não encontrado. {ex.Message}" });
        }
       
    }

    //Criar autor
    [HttpPost]
    public async Task<ActionResult> CreateAuthor(AuthorRequestDTO author)
    {
        try
        {
            await authorService.CreateAuthorAsync(author);
            return Created();
        }
        catch (Exception ex)
        {
            return StatusCode(400, new { error = $"Erro ao criar autor. {ex.Message}" });
        }
    }

    //Atualizar autor
    // PATCH 
    [HttpPatch("{id}")]
    public async Task<ActionResult> UpdatePatchAuthor(Guid id, [FromBody] UpdateAuthorParcialDTO dto)
    {
        try
        {
            await authorService.UpdatePatchAuthorAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(400, new { error = $"Erro ao atualizar autor. {ex.Message}" });
        }
    }

    // PUT 
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdatePutAuthor(Guid id, [FromBody] UpdateAuthorFullDTO dto)
    {
        try
        {
            await authorService.UpdatePutAuthorAsync(id, dto);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(400, new { error = $"Erro ao atualizar autor. {ex.Message}" });
        }
    }

    // Atualiza apenas o tipo do autor// Ativo (1) ou Inativo (2)
    [HttpPut("type/{id}")]
    public async Task<ActionResult> UpdateType(Guid id, [FromBody] AuthorType type)
    {
        try
        {
            await authorService.UpdateAuthorTypeAsync(id, type);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(400, new { error = $"Erro ao atualizar tipo do autor. {ex.Message}" });
        }
    }

    //Listar cursos do autor
    [HttpGet("{id}/courses")]
    public async Task<ActionResult> GetAuthorCourses(Guid id)
    {
        var result = await authorService.GetAuthorCoursesAsync(id);

        if (result == null)
            return NotFound("Autor não encontrado.");

        return Ok(result);
    }
}
