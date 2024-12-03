using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Renamed_Bounded_Context : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceAmount",
                schema: "Gallery",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                schema: "Gallery",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "PricePrecision",
                schema: "Gallery",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "PriceSymbol",
                schema: "Gallery",
                table: "CartItems");

            migrationBuilder.EnsureSchema(
                name: "Carts");

            migrationBuilder.RenameTable(
                name: "Carts",
                schema: "Gallery",
                newName: "Carts",
                newSchema: "Carts");

            migrationBuilder.RenameTable(
                name: "CartItems",
                schema: "Gallery",
                newName: "CartItems",
                newSchema: "Carts");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                schema: "Carts",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "Carts",
                table: "CartItems");

            migrationBuilder.EnsureSchema(
                name: "Gallery");

            migrationBuilder.RenameTable(
                name: "Carts",
                schema: "Carts",
                newName: "Carts",
                newSchema: "Gallery");

            migrationBuilder.RenameTable(
                name: "CartItems",
                schema: "Carts",
                newName: "CartItems",
                newSchema: "Gallery");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceAmount",
                schema: "Gallery",
                table: "CartItems",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "PriceCurrency",
                schema: "Gallery",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PricePrecision",
                schema: "Gallery",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "PriceSymbol",
                schema: "Gallery",
                table: "CartItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
