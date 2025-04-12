namespace MesCoursesApi.Dto;

public class ShoppingListDto
{
    public int? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<ShoppingListLineDto>? Lines { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}