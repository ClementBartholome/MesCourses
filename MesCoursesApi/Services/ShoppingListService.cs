using ApiV2.Data;
using MesCoursesApi.Dto;
using MesCoursesApi.Enums;
using MesCoursesApi.Extensions;
using MesCoursesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MesCoursesApi.Services;

public class ShoppingListService(AppDbContext context)
{
    public async Task<List<ShoppingListDto>> GetAllAsync()
    {
        return await context.ShoppingLists
            .Include(sl => sl.Lines)
            .ThenInclude(line => line.Ingredient)
            .Select(sl => new ShoppingListDto
            {
                Id = sl.Id,
                CreatedAt = sl.CreatedAt,
                UpdatedAt = sl.UpdatedAt,
                Name = sl.Name,
                Lines = sl.Lines.Select(line => new ShoppingListLineDto
                {
                    Id = line.Id,
                    ShoppingListId = line.ShoppingListId,
                    IngredientId = line.IngredientId,
                    Quantity = line.Quantity,
                    IsChecked = line.IsChecked,
                    IngredientName = line.Ingredient.Name,
                    CategoryName = line.Ingredient.Category.Name,
                    Unit = line.Unit.GetDescription()
                }).ToList()
            })
            .ToListAsync();
    }

    public async Task<ShoppingListDto?> GetByIdAsync(int id)
    {
        return await context.ShoppingLists
            .Include(sl => sl.Lines)
            .ThenInclude(line => line.Ingredient)
            .Where(sl => sl.Id == id)
            .Select(sl => new ShoppingListDto
            {
                Id = sl.Id,
                CreatedAt = sl.CreatedAt,
                UpdatedAt = sl.UpdatedAt,
                Name = sl.Name,
                Lines = sl.Lines.Select(line => new ShoppingListLineDto
                {
                    Id = line.Id,
                    ShoppingListId = line.ShoppingListId,
                    IngredientId = line.IngredientId,
                    Quantity = line.Quantity,
                    IsChecked = line.IsChecked,
                    IngredientName = line.Ingredient.Name,
                    CategoryName = line.Ingredient.Category.Name,
                    Unit = line.Unit.GetDescription()
                }).ToList()
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ShoppingListDto> CreateAsync(ShoppingListDto dto)
    {
        var shoppingList = new ShoppingList
        {
            Lines = dto.Lines?.Select(line => new ShoppingListLine
            {
                IngredientId = line.IngredientId,
                Quantity = line.Quantity,
                IsChecked = line.IsChecked,
                Unit = line.Unit.ToEnum<UnitEnum>()
            }).ToList()
        };

        context.ShoppingLists.Add(shoppingList);
        await context.SaveChangesAsync();

        dto.Id = shoppingList.Id;
        dto.CreatedAt = shoppingList.CreatedAt;
        dto.UpdatedAt = shoppingList.UpdatedAt;
        return dto;
    }

    public async Task<bool> UpdateAsync(int id, ShoppingListDto dto)
    {
        var shoppingList = await context.ShoppingLists
            .Include(sl => sl.Lines)
            .FirstOrDefaultAsync(sl => sl.Id == id);

        if (shoppingList == null) return false;

        // Mise à jour du nom de la liste si fourni
        if (!string.IsNullOrEmpty(dto.Name))
        {
            shoppingList.Name = dto.Name;
        }

        // Mise à jour des lignes si elles sont fournies
        if (dto.Lines != null && dto.Lines.Any())
        {
            foreach (var lineDto in dto.Lines)
            {
                var existingLine = shoppingList.Lines.FirstOrDefault(l => l.Id == lineDto.Id);
                if (existingLine != null)
                {
                    // Mise à jour de la ligne existante
                    existingLine.Quantity = lineDto.Quantity;
                    existingLine.IsChecked = lineDto.IsChecked;
                    existingLine.Unit = lineDto.Unit.ToEnum<UnitEnum>();
                }
                else
                {
                    shoppingList.Lines.Add(new ShoppingListLine
                    {
                        IngredientId = lineDto.IngredientId,
                        Quantity = lineDto.Quantity,
                        IsChecked = lineDto.IsChecked,
                        Unit = lineDto.Unit.ToEnum<UnitEnum>()
                    });
                }
            }
        }

        shoppingList.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var shoppingList = await context.ShoppingLists.FindAsync(id);
        if (shoppingList == null) return false;

        context.ShoppingLists.Remove(shoppingList);
        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<ShoppingListLineDto?> AddLineAsync(int shoppingListId, ShoppingListLineDto lineDto)
    {
        var shoppingList = await context.ShoppingLists
            .Include(sl => sl.Lines)
            .ThenInclude(line => line.Ingredient)
            .FirstOrDefaultAsync(sl => sl.Id == shoppingListId);

        if (shoppingList == null) return null;

        var newLine = new ShoppingListLine
        {
            ShoppingListId = shoppingListId,
            IngredientId = lineDto.IngredientId,
            Quantity = lineDto.Quantity,
            IsChecked = lineDto.IsChecked,
            Unit = lineDto.Unit.ToEnum<UnitEnum>()
        };

        shoppingList.Lines.Add(newLine);
        shoppingList.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();

        return new ShoppingListLineDto
        {
            Id = newLine.Id,
            ShoppingListId = newLine.ShoppingListId,
            IngredientId = newLine.IngredientId,
            Quantity = newLine.Quantity,
            IsChecked = newLine.IsChecked,
            Unit = newLine.Unit.GetDescription(),
            IngredientName = lineDto.IngredientName,
            CategoryName = lineDto.CategoryName
        };
    }
    
    public async Task<bool> UpdateLineAsync(int shoppingListId, int lineId, ShoppingListLineDto lineDto)
    {
        var shoppingList = await context.ShoppingLists
            .Include(sl => sl.Lines)
            .FirstOrDefaultAsync(sl => sl.Id == shoppingListId);

        if (shoppingList == null) return false;

        var line = shoppingList.Lines.FirstOrDefault(l => l.Id == lineId);
        if (line == null) return false;

        line.Quantity = lineDto.Quantity;
        line.IsChecked = lineDto.IsChecked;
        shoppingList.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> ToggleLineCheckedAsync(int shoppingListId, int lineId, bool isChecked)
    {
        var shoppingList = await context.ShoppingLists
            .Include(sl => sl.Lines)
            .FirstOrDefaultAsync(sl => sl.Id == shoppingListId);

        if (shoppingList == null) return false;

        var line = shoppingList.Lines.FirstOrDefault(l => l.Id == lineId);
        if (line == null) return false;

        line.IsChecked = isChecked;
        shoppingList.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return true;
    }
    
    public async Task<bool> DeleteLineAsync(int shoppingListId, int lineId)
    {
        var shoppingList = await context.ShoppingLists
            .Include(sl => sl.Lines)
            .FirstOrDefaultAsync(sl => sl.Id == shoppingListId);

        if (shoppingList == null) return false;

        var line = shoppingList.Lines.FirstOrDefault(l => l.Id == lineId);
        if (line == null) return false;

        shoppingList.Lines.Remove(line);
        shoppingList.UpdatedAt = DateTime.UtcNow;

        await context.SaveChangesAsync();
        return true;
    }
}