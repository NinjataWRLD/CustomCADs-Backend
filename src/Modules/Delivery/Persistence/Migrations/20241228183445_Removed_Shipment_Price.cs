using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Delivery.Persistence.Migrations;

/// <inheritdoc />
public partial class Removed_Shipment_Price : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Price",
            schema: "Delivery",
            table: "Shipments");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(
            name: "Price",
            schema: "Delivery",
            table: "Shipments",
            type: "numeric",
            nullable: false,
            defaultValue: 0m);
    }
}
