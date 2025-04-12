namespace MesCoursesApi.Dto;

public class ShoppingListLineDto
{
    public int Id { get; set; }
    public int ShoppingListId { get; set; }
    public int IngredientId { get; set; }
    public int Quantity { get; set; }
    public bool IsChecked { get; set; } = false;
    public string? IngredientName { get; set; }
    public string? CategoryName { get; set; } 
    public string Unit { get; set; } = string.Empty;

}