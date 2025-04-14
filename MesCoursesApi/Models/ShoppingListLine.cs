using MesCoursesApi.Enums;

namespace MesCoursesApi.Models;

public class ShoppingListLine
{
    public int Id { get; set; }
    public int ShoppingListId { get; set; }
    public int IngredientId { get; set; }
    public decimal Quantity { get; set; }
    public bool IsChecked { get; set; } = false;
    public UnitEnum Unit { get; set; } = UnitEnum.Piece;
    public Ingredient Ingredient { get; set; }
    public ShoppingList ShoppingList { get; set; }
}