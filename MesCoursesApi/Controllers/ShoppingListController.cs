using MesCoursesApi.Dto;
using MesCoursesApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MesCoursesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingListController(ShoppingListService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var shoppingLists = await service.GetAllAsync();
        return Ok(shoppingLists);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var shoppingList = await service.GetByIdAsync(id);
        if (shoppingList == null) return NotFound();
        return Ok(shoppingList);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ShoppingListDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdShoppingList = await service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = createdShoppingList.Id }, createdShoppingList);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] ShoppingListDto dto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var success = await service.UpdateAsync(id, dto);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await service.DeleteAsync(id);
        if (!success) return NotFound();
        return NoContent();
    }
    
    [HttpPost("{id:int}/lines")]
    public async Task<IActionResult> AddLine(int id, [FromBody] ShoppingListLineDto lineDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdLine = await service.AddLineAsync(id, lineDto);
        if (createdLine == null) return NotFound();

        return CreatedAtAction(nameof(GetById), new { id = createdLine.ShoppingListId }, createdLine);
    }
    
    [HttpPost("{id:int}/lines/batch")]
    public async Task<IActionResult> AddMultipleLines(int id, [FromBody] List<ShoppingListLineDto> linesDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var createdLines = await service.AddMultipleLinesAsync(id, linesDto);
        if (createdLines == null) return NotFound();

        return CreatedAtAction(nameof(GetById), new { id = createdLines.First().ShoppingListId }, createdLines);
    }

    [HttpPut("{id:int}/lines/{lineId:int}")]
    public async Task<IActionResult> UpdateLine(int id, int lineId, [FromBody] ShoppingListLineDto lineDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var success = await service.UpdateLineAsync(id, lineId, lineDto);
        if (!success) return NotFound();
        return NoContent();
    }
    
    [HttpPatch("{id:int}/lines/{lineId:int}/check")]
    public async Task<IActionResult> ToggleLineChecked(int id, int lineId, [FromBody] bool isChecked)
    {
        var success = await service.ToggleLineCheckedAsync(id, lineId, isChecked);
        if (!success) return NotFound();
        return NoContent();
    }
    
    [HttpDelete("{id:int}/lines/{lineId:int}")]
    public async Task<IActionResult> DeleteLine(int id, int lineId)
    {
        var success = await service.DeleteLineAsync(id, lineId);
        if (!success) return NotFound();
        return NoContent();
    }
}