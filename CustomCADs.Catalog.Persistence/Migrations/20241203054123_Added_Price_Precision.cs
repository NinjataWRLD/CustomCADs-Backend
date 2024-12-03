using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Catalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_Price_Precision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "Catalog",
                table: "Products",
                type: "decimal(19,2)",
                precision: 19,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                schema: "Catalog",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(19,2)",
                oldPrecision: 19,
                oldScale: 2);
        }
    }
}
