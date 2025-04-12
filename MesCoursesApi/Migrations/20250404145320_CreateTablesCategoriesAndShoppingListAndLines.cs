using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablesCategoriesAndShoppingListAndLines : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "Ingredients");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Ingredients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "IngredientCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingLists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingListLines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ShoppingListId = table.Column<int>(type: "integer", nullable: false),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    IsChecked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingListLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingListLines_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoppingListLines_ShoppingLists_ShoppingListId",
                        column: x => x.ShoppingListId,
                        principalTable: "ShoppingLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "IngredientCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Fruits" },
                    { 2, "Légumes" },
                    { 3, "Viandes" },
                    { 4, "Produits laitiers" },
                    { 5, "Céréales" },
                    { 6, "Épices" }
                });

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 1, "Pomme" });

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 1, "Banane" });

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { 2, "Carotte" });

            migrationBuilder.InsertData(
                table: "Ingredients",
                columns: new[] { "Id", "CategoryId", "Name" },
                values: new object[,]
                {
                    { 4, 2, "Brocoli" },
                    { 5, 3, "Poulet" },
                    { 6, 3, "Boeuf" },
                    { 7, 4, "Lait" },
                    { 8, 4, "Yaourt" },
                    { 9, 5, "Riz" },
                    { 10, 5, "Pâtes" },
                    { 11, 6, "Sel" },
                    { 12, 6, "Poivre" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_CategoryId",
                table: "Ingredients",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListLines_IngredientId",
                table: "ShoppingListLines",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingListLines_ShoppingListId",
                table: "ShoppingListLines",
                column: "ShoppingListId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_IngredientCategories_CategoryId",
                table: "Ingredients",
                column: "CategoryId",
                principalTable: "IngredientCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_IngredientCategories_CategoryId",
                table: "Ingredients");

            migrationBuilder.DropTable(
                name: "IngredientCategories");

            migrationBuilder.DropTable(
                name: "ShoppingListLines");

            migrationBuilder.DropTable(
                name: "ShoppingLists");

            migrationBuilder.DropIndex(
                name: "IX_Ingredients_CategoryId",
                table: "Ingredients");

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Ingredients");

            migrationBuilder.AddColumn<string>(
                name: "Category",
                table: "Ingredients",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Category", "Name" },
                values: new object[] { "Légumes", "Tomate" });

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Category", "Name" },
                values: new object[] { "Fruits", "Pomme" });

            migrationBuilder.UpdateData(
                table: "Ingredients",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Category", "Name" },
                values: new object[] { "Produits laitiers", "Lait" });
        }
    }
}
