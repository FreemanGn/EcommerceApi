using AutoMapper;
using EcommerceApi.Dtos;
using EcommerceApi.Models;
using EcommerceApi.Services;
using EcommerceApi.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;        
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<CategoryDto>>> GetAllCategories()
        {
            var categoryDtos = await _categoryService.GetAllAsync();

            return Ok(categoryDtos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetDetailCategory(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Please Provide a valid Category Id");
                }
                var categoryDto = await _categoryService.GetByIdAsync(id);

                if (categoryDto == null)
                {
                    return NotFound($"No Category was found with the given Id {id}");
                }
                return Ok(categoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occured ! Please try again later");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromBody] CategoryDto categoryDto)
        {
            try
            {
                
                if (await _categoryService.CategoryExists(categoryDto.Id))
                {
                    return Conflict("Category Already exists");
                }

                // add category
                var category = await _categoryService.AddAsync(categoryDto);

                // The category is successfully added, return the created category using get category details method
                return CreatedAtAction(nameof(GetDetailCategory), new { Id = category.Id }, category);
            }
            catch (Exception ex)
            {

                // Return Server error message
                return StatusCode(500, "An error occurred! Please try again later");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            try
            {
                if (categoryDto.Id <= 0 || id != categoryDto.Id)
                {
                    return BadRequest("Please Provide a valid Category Id");
                }

                await _categoryService.UpdateAsync(categoryDto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred! Please try again later");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            try
            {
                // note that id = 0 means that the id was not provided (null)
                if (id <= 0)
                {
                    return BadRequest("Please provide an id of the category to delete");
                }

                var category = await _categoryService.GetByIdAsync(id);

                if (category == null)
                {
                    return NotFound("The requested category to delete was not found");
                }

                return Ok("category was successfully Deleted");
            }
            catch
            {
                return StatusCode(500, "An error occurred! Please try again later");
            }
        }

    }
}
