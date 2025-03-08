using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_CustomizationId : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Weight",
            schema: "Carts",
            table: "ActiveCartItems");

        migrationBuilder.AddColumn<Guid>(
            name: "CustomizationId",
            schema: "Carts",
            table: "PurchasedCartItems",
            type: "uuid",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "CustomizationId",
            schema: "Carts",
            table: "ActiveCartItems",
            type: "uuid",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CustomizationId",
            schema: "Carts",
            table: "PurchasedCartItems");

        migrationBuilder.DropColumn(
            name: "CustomizationId",
            schema: "Carts",
            table: "ActiveCartItems");

        migrationBuilder.AddColumn<double>(
            name: "Weight",
            schema: "Carts",
            table: "ActiveCartItems",
            type: "double precision",
            precision: 6,
            scale: 2,
            nullable: false,
            defaultValue: 0.0);
    }
}
