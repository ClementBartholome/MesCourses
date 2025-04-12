using ApiV2.Data;
using MesCoursesApi.Dto;
using MesCoursesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MesCoursesApi.Services;

public class IngredientsService(AppDbContext context)
{
    public async Task<List<IngredientDto>> GetAllAsync()
    {
        return await context.Ingredients
            .Include(i => i.Category)
            .Select(i => new IngredientDto
            {
                Id = i.Id,
                Name = i.Name,
                CategoryId = i.CategoryId,
                CategoryName = i.Category.Name
            })
            .ToListAsync();
    }

    public async Task<IngredientDto?> GetByIdAsync(int id)
    {
        return await context.Ingredients
            .Include(i => i.Category)
            .Where(i => i.Id == id)
            .Select(i => new IngredientDto
            {
                Id = i.Id,
                Name = i.Name,
                CategoryId = i.CategoryId,
                CategoryName = i.Category.Name
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IngredientDto> CreateAsync(IngredientDto dto)
    {
        var ingredient = new Ingredient
        {
            Name = dto.Name,
            CategoryId = dto.CategoryId
        };

        context.Ingredients.Add(ingredient);
        await context.SaveChangesAsync();

        dto.Id = ingredient.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, IngredientDto dto)
    {
        var ingredient = await context.Ingredients.FindAsync(id);
        if (ingredient == null) return false;

        ingredient.Name = dto.Name;
        ingredient.CategoryId = dto.CategoryId;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ingredient = await context.Ingredients.FindAsync(id);
        if (ingredient == null) return false;

        context.Ingredients.Remove(ingredient);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<IngredientDto>> SearchAsync(string searchTerm)
    {
        return await context.Ingredients
            .Include(i => i.Category)
            .Where(i => EF.Functions.Like(i.Name.ToLower(), $"%{searchTerm.ToLower()}%"))
            .Select(i => new IngredientDto
            {
                Id = i.Id,
                Name = i.Name,
                CategoryId = i.CategoryId,
                CategoryName = i.Category.Name
            })
            .ToListAsync();
    }
}