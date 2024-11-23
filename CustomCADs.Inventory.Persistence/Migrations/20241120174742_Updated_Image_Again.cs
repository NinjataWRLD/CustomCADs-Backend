using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Inventory.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Image_Again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                schema: "Catalog",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContentType",
                schema: "Catalog",
                table: "Products");
        }
    }
}
