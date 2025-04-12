using System.ComponentModel.DataAnnotations.Schema;

namespace MesCoursesApi.Models;

public class IngredientCategory
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Name { get; set; }
}