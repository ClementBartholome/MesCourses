using MesCoursesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiV2.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<IngredientCategory> IngredientCategories { get; set; }
    public DbSet<ShoppingList> ShoppingLists { get; set; }
    public DbSet<ShoppingListLine> ShoppingListLines { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingredient>()
            .Property(i => i.Id)
            .ValueGeneratedOnAdd();
        
        // Seed some initial data
        modelBuilder.Entity<IngredientCategory>().HasData(
            new IngredientCategory { Id = 1, Name = "Fruits" },
            new IngredientCategory { Id = 2, Name = "Légumes" },
            new IngredientCategory { Id = 3, Name = "Viandes" },
            new IngredientCategory { Id = 4, Name = "Produits laitiers" },
            new IngredientCategory { Id = 5, Name = "Céréales" },
            new IngredientCategory { Id = 6, Name = "Épices" },
            new IngredientCategory { Id = 7, Name = "Condiments" },
            new IngredientCategory { Id = 8, Name = "Huiles" },
            new IngredientCategory { Id = 9, Name = "Sauces" },
            new IngredientCategory { Id = 10, Name = "Herbes" },
            new IngredientCategory { Id = 11, Name = "Poissons" },
            new IngredientCategory { Id = 12, Name = "Fruits de mer" },
            new IngredientCategory { Id = 13, Name = "Pains et pâtisseries" },
            new IngredientCategory { Id = 14, Name = "Boissons" },
            new IngredientCategory { Id = 15, Name = "Snacks" }
        );
    
        modelBuilder.Entity<Ingredient>().HasData(
            new Ingredient { Id = 1, Name = "Pomme", CategoryId = 1 },
            new Ingredient { Id = 2, Name = "Banane", CategoryId = 1 },
            new Ingredient { Id = 3, Name = "Carotte", CategoryId = 2 },
            new Ingredient { Id = 4, Name = "Brocoli", CategoryId = 2 },
            new Ingredient { Id = 5, Name = "Poulet", CategoryId = 3 },
            new Ingredient { Id = 6, Name = "Boeuf", CategoryId = 3 },
            new Ingredient { Id = 7, Name = "Lait", CategoryId = 4 },
            new Ingredient { Id = 8, Name = "Yaourt", CategoryId = 4 },
            new Ingredient { Id = 9, Name = "Riz", CategoryId = 5 },
            new Ingredient { Id = 10, Name = "Pâtes", CategoryId = 5 },
            new Ingredient { Id = 11, Name = "Sel", CategoryId = 6 },
            new Ingredient { Id = 12, Name = "Poivre", CategoryId = 6 },
            new Ingredient { Id = 13, Name = "Saumon", CategoryId = 11 },
            new Ingredient { Id = 14, Name = "Thon", CategoryId = 11 },
            new Ingredient { Id = 15, Name = "Crevettes", CategoryId = 12 },
            new Ingredient { Id = 16, Name = "Moules", CategoryId = 12 },
            new Ingredient { Id = 17, Name = "Pain", CategoryId = 13 },
            new Ingredient { Id = 18, Name = "Croissant", CategoryId = 13 },
            new Ingredient { Id = 19, Name = "Eau", CategoryId = 14 },
            new Ingredient { Id = 20, Name = "Jus d'orange", CategoryId = 14 },
            new Ingredient { Id = 21, Name = "Chips", CategoryId = 15 },
            new Ingredient { Id = 22, Name = "Chocolat", CategoryId = 15 },
            new Ingredient { Id = 23, Name = "Parmesan", CategoryId = 4 },
            new Ingredient { Id = 24, Name = "Chèvre", CategoryId = 4 },
            new Ingredient { Id = 25, Name = "Mozzarella", CategoryId = 4 },
            new Ingredient { Id = 26, Name = "Brie", CategoryId = 4 },
            new Ingredient { Id = 27, Name = "Roquefort", CategoryId = 4 },
            new Ingredient { Id = 28, Name = "Comté", CategoryId = 4 },
            new Ingredient { Id = 29, Name = "Feta", CategoryId = 4 },
            new Ingredient { Id = 30, Name = "Ricotta", CategoryId = 4 },
            new Ingredient { Id = 31, Name = "Crème fraîche", CategoryId = 4 },
            new Ingredient { Id = 32, Name = "Beurre", CategoryId = 4 }
        );
    }
}