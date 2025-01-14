using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Catalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Removed_Product_Likes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                schema: "Catalog",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                schema: "Catalog",
                table: "Products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
