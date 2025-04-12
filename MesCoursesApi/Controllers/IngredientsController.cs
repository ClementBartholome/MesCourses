using MesCoursesApi.Dto;
using MesCoursesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesCoursesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class IngredientsController(IngredientsService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var ingredients = await service.GetAllAsync();
        return Ok(ingredients);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var ingredient = await service.GetByIdAsync(id);
        if (ingredient == null) return NotFound();
        return Ok(ingredient);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] IngredientDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdIngredient = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdIngredient.Id }, createdIngredient);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] IngredientDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var success = await service.UpdateAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await service.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
    
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string term)
    {
        if (string.IsNullOrWhiteSpace(term)) return BadRequest("Le terme de recherche ne peut pas être vide.");

        var results = await service.SearchAsync(term);
        return Ok(results);
    }
}