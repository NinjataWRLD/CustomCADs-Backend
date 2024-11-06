using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Catalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Catalog");

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

            migrationBuilder.CreateTable(
                name: "Products",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(18)", maxLength: 18, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(750)", maxLength: 750, nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CadPath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CamCoordX = table.Column<int>(type: "int", nullable: false),
                    CamCoordY = table.Column<int>(type: "int", nullable: false),
                    CamCoordZ = table.Column<int>(type: "int", nullable: false),
                    PanCoordX = table.Column<int>(type: "int", nullable: false),
                    PanCoordY = table.Column<int>(type: "int", nullable: false),
                    PanCoordZ = table.Column<int>(type: "int", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "Catalog",
                        principalTable: "Categories",
                        principalColumn: "Id");
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products",
                schema: "Catalog");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "Catalog");
        }
    }
}
