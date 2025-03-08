using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations;

/// <inheritdoc />
public partial class Changed_DeliveryType_To_Delivery : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "DeliveryType",
            schema: "Carts",
            table: "CartItems");

        migrationBuilder.AddColumn<bool>(
            name: "Delivery",
            schema: "Carts",
            table: "CartItems",
            type: "boolean",
            nullable: false,
            defaultValue: false);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Delivery",
            schema: "Carts",
            table: "CartItems");

        migrationBuilder.AddColumn<string>(
            name: "DeliveryType",
            schema: "Carts",
            table: "CartItems",
            type: "text",
            nullable: false,
            defaultValue: "");
    }
}
