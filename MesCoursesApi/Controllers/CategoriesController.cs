using MesCoursesApi.Dto;
using MesCoursesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesCoursesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoriesController : ControllerBase
{
    private readonly CategoriesService _service;

    public CategoriesController(CategoriesService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _service.GetAllAsync();
        return Ok(categories);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var category = await _service.GetByIdAsync(id);
        if (category == null) return NotFound();

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IngredientCategoryDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdCategory = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdCategory.Id }, createdCategory);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] IngredientCategoryDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, dto);
        if (!updated) return NotFound();

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);
        if (!deleted) return NotFound();

        return NoContent();
    }
}