using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Inventory.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_Purchases_Likes_And_Views_To_Products : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Likes",
                schema: "Inventory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Purchases",
                schema: "Inventory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Views",
                schema: "Inventory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Likes",
                schema: "Inventory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Purchases",
                schema: "Inventory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Views",
                schema: "Inventory",
                table: "Products");
        }
    }
}
