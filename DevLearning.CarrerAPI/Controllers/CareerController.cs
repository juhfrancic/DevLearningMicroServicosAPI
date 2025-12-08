using DevLearning.CareerAPI.Services;
using DevLearning.CareerAPI.Services.Interfaces;
using Domain.Models.DTOs.CareerItem;
using Domain.Models.DTOs.Carrer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DevLearning.CareerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CareerController : ControllerBase
    {
        public readonly ICareerService _careerService;

        public CareerController(ICareerService careerService)
        {
            this._careerService = careerService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateCareerAsync([FromBody] CareerRequestDTO careerDTO)
        {
            try
            {
                await _careerService.CreateCareerAsync(careerDTO);
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

        [HttpGet]
        public async Task<ActionResult<List<CareerResponseDto>>> GetAllCareersAsync()
        {
            try
            {
                var careers = (await _careerService.GetAllCareersAsync()).ToList();

                if (careers.Count is 0)
                    return NotFound("Register not found!");

                return Ok(careers);
                
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

        [HttpGet("{careerId}")]
        public async Task<ActionResult<CareerResponseDto>> GetCareerByIdAsync(string careerId)
        {
            try
            {
                var careerData = await _careerService.GetCareerByIdAsync(careerId);
                return Ok(careerData);
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

        [HttpPut("{careerId}")]
        public async Task<ActionResult> UpdateCareerAsync(string careerId, [FromBody] CareerUpdateDTO careerDTO)
        {
            try
            {
                await _careerService.UpdateCareerAsync(careerId, careerDTO);
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

        [HttpDelete("{careerId}")]
        public async Task<ActionResult> UpdateActiveCareerAsync(string careerId)
        {
            try
            {
                await _careerService.UpdateActiveCareerAsync(careerId);
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

        [HttpPost("{careerId}/items")]
        public async Task<ActionResult> AddItemCareerAsync(string careerId, [FromBody] CareerItemRequestDTO careerItemDTO)
        {
            try
            {
                await _careerService.AddItemCareerAsync(careerId, careerItemDTO);
                return Created();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{careerId}/items/{courseId}")]
        public async Task<ActionResult> RemoveItemCareerAsync(string careerId, string courseId)
        {
            try
            {
                await _careerService.RemoveItemCareerAsync(careerId, courseId);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}
