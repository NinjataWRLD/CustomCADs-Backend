using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Inventory.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Simplified_Price : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAmount",
                schema: "Inventory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                schema: "Inventory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PricePrecision",
                schema: "Inventory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PriceSymbol",
                schema: "Inventory",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "Inventory",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Inventory",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAmount",
                schema: "Inventory",
                table: "Products",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PriceCurrency",
                schema: "Inventory",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PricePrecision",
                schema: "Inventory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PriceSymbol",
                schema: "Inventory",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
