using ApiV2.Data;
using MesCoursesApi.Dto;
using MesCoursesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MesCoursesApi.Services;

public class CategoriesService(AppDbContext context)
{
    public async Task<List<IngredientCategoryDto>> GetAllAsync()
    {
        return await context.IngredientCategories
            .Select(c => new IngredientCategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync();
    }

    public async Task<IngredientCategoryDto?> GetByIdAsync(int id)
    {
        var category = await context.IngredientCategories.FindAsync(id);
        if (category == null) return null;

        return new IngredientCategoryDto
        {
            Id = category.Id,
            Name = category.Name
        };
    }

    public async Task<IngredientCategoryDto> CreateAsync(IngredientCategoryDto dto)
    {
        var category = new IngredientCategory
        {
            Name = dto.Name
        };

        context.IngredientCategories.Add(category);
        await context.SaveChangesAsync();

        dto.Id = category.Id;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, IngredientCategoryDto dto)
    {
        var category = await context.IngredientCategories.FindAsync(id);
        if (category == null) return false;

        category.Name = dto.Name;
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await context.IngredientCategories.FindAsync(id);
        if (category == null) return false;

        context.IngredientCategories.Remove(category);
        await context.SaveChangesAsync();
        return true;
    }
}