using DevLearning.CourseAPI.Services;
using Domain.Models.DTOs.Course;
using Microsoft.AspNetCore.Mvc;

namespace DevLearning.CourseAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController(
    CourseService service
    ) : ControllerBase
{
    private readonly CourseService _courseService = service;

    [HttpGet]
    public async Task<ActionResult<List<CourseResponseDTO>>> GetAllCoursesAsync([FromQuery] string? category)
    {
        try
        {
            var courses = await _courseService.GetAllCoursesAsync(category);

            if (courses.Count is 0)
                return NotFound("Register not found!");

            return Ok(courses);
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpDelete("{title}")]
    public async Task<ActionResult> DeleteCourseByTitleAsync(string title)
    {
        try
        {
            await _courseService.DeleteCourseByTitleAsync(title);
            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpGet("get-by-title")]
    public async Task<ActionResult<CourseResponseDTO>> GetOneCourseByTitleAsync([FromBody]CourseRequestTitleDTO course)
    {
        try
        {
            var user = await _courseService.GetOneCourseByTitleAsync(course.Title);
            return Ok(user);
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPost()]
    public async Task<ActionResult> CreateUserAsync(CourseRequestDTO course)
    {
        try
        {
            await _courseService.CreateCourseAsync(course);
            return Created();
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPut("{title}")]
    public async Task<IActionResult> UpdateCourseByTitleAsync(string title, CourseUpdateDTO update)
    {
        try
        {
            await _courseService.UpdateCourseByTitleAsync(title, update);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }

    [HttpPut("Active/{title}")]
    public async Task<IActionResult> UpdateActiveCourseByTitleAsync(string title)
    {
        try
        {
            await _courseService.UpdateActiveCourseByTitleAsync(title);
            return Ok();
        }
        catch (ArgumentException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(400, new { error = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = ex.Message });
        }
    }
}
