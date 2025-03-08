using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Delivery.Persistence.Migrations;

/// <inheritdoc />
public partial class Erased_Shipment_Street : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Street",
            schema: "Delivery",
            table: "Shipments");

        migrationBuilder.RenameColumn(
            name: "ClientId",
            schema: "Delivery",
            table: "Shipments",
            newName: "BuyerId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "BuyerId",
            schema: "Delivery",
            table: "Shipments",
            newName: "ClientId");

        migrationBuilder.AddColumn<string>(
            name: "Street",
            schema: "Delivery",
            table: "Shipments",
            type: "text",
            nullable: false,
            defaultValue: "");
    }
}
