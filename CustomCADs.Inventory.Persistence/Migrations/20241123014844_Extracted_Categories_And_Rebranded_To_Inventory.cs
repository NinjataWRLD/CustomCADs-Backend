using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Inventory.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Extracted_Categories_And_Rebranded_To_Inventory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Catalog");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.EnsureSchema(
                name: "Inventory");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "Catalog",
                newName: "Products",
                newSchema: "Inventory");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

            migrationBuilder.RenameTable(
                name: "Products",
                schema: "Inventory",
                newName: "Products",
                newSchema: "Catalog");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Catalog",
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Animals" },
                    { 2, "Characters" },
                    { 3, "Electronics" },
                    { 4, "Fashion" },
                    { 5, "Furniture" },
                    { 6, "Nature" },
                    { 7, "Science" },
                    { 8, "Sports" },
                    { 9, "Toys" },
                    { 10, "Vehicles" },
                    { 11, "Others" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                schema: "Catalog",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_CategoryId",
                schema: "Catalog",
                table: "Products",
                column: "CategoryId",
                principalSchema: "Catalog",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
