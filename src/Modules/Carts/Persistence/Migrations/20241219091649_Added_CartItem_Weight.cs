using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_CartItem_Weight : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<decimal>(
            name: "Price",
            schema: "Carts",
            table: "CartItems",
            type: "numeric(10,2)",
            precision: 10,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(19,2)",
            oldPrecision: 19,
            oldScale: 2);

        migrationBuilder.AddColumn<double>(
            name: "Weight",
            schema: "Carts",
            table: "CartItems",
            type: "double precision",
            precision: 6,
            scale: 2,
            nullable: false,
            defaultValue: 0.0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Weight",
            schema: "Carts",
            table: "CartItems");

        migrationBuilder.AlterColumn<decimal>(
            name: "Price",
            schema: "Carts",
            table: "CartItems",
            type: "numeric(19,2)",
            precision: 19,
            scale: 2,
            nullable: false,
            oldClrType: typeof(decimal),
            oldType: "numeric(10,2)",
            oldPrecision: 10,
            oldScale: 2);
    }
}
