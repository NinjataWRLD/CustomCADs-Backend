using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Catalog.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial_Migration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Catalog");

        migrationBuilder.CreateTable(
            name: "Products",
            schema: "Catalog",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
                Description = table.Column<string>(type: "character varying(750)", maxLength: 750, nullable: false),
                Price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                Status = table.Column<string>(type: "text", nullable: false),
                UploadDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CategoryId = table.Column<int>(type: "integer", nullable: false),
                CadId = table.Column<Guid>(type: "uuid", nullable: false),
                CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                DesignerId = table.Column<Guid>(type: "uuid", nullable: false),
                Likes = table.Column<int>(type: "integer", nullable: false),
                Purchases = table.Column<int>(type: "integer", nullable: false),
                Views = table.Column<int>(type: "integer", nullable: false),
                ImageContentType = table.Column<string>(type: "text", nullable: false),
                ImageKey = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Products", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Products",
            schema: "Catalog");
    }
}
