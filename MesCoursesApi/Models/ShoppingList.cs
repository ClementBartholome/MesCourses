namespace MesCoursesApi.Models;

public class ShoppingList
{
    public int Id { get; set; }
    public string Name { get; set; } = $"Liste du {DateTime.UtcNow:dd/MM/yyyy}";
    public List<ShoppingListLine>? Lines { get; set; } = [];
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}