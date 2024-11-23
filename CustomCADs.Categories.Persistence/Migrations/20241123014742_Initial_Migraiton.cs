using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Categories.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migraiton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Categories");

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "Categories",
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
                schema: "Categories",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Categories");
        }
    }
}
