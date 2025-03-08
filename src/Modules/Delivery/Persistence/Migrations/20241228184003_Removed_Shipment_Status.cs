using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Delivery.Persistence.Migrations;

/// <inheritdoc />
public partial class Removed_Shipment_Status : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Status",
            schema: "Delivery",
            table: "Shipments");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Status",
            schema: "Delivery",
            table: "Shipments",
            type: "text",
            nullable: false,
            defaultValue: "");
    }
}
