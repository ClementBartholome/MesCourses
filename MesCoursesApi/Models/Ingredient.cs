namespace MesCoursesApi.Models;

public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public IngredientCategory Category { get; set; }
}