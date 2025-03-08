using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations;

/// <inheritdoc />
public partial class Renamed_Delivery_To_ForDelivery : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Delivery",
            schema: "Carts",
            table: "PurchasedCartItems",
            newName: "ForDelivery");

        migrationBuilder.RenameColumn(
            name: "Delivery",
            schema: "Carts",
            table: "ActiveCartItems",
            newName: "ForDelivery");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "ForDelivery",
            schema: "Carts",
            table: "PurchasedCartItems",
            newName: "Delivery");

        migrationBuilder.RenameColumn(
            name: "ForDelivery",
            schema: "Carts",
            table: "ActiveCartItems",
            newName: "Delivery");
    }
}
