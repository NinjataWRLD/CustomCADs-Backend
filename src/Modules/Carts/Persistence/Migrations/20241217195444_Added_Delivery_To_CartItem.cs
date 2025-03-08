using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_Delivery_To_CartItem : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
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
    }
}
