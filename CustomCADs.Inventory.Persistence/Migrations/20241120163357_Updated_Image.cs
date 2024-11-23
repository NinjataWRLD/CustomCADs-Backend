using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Inventory.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                schema: "Catalog",
                table: "Products",
                newName: "ImageKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageKey",
                schema: "Catalog",
                table: "Products",
                newName: "ImagePath");
        }
    }
}
