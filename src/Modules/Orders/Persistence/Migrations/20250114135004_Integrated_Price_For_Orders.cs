using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Orders.Persistence.Migrations;

/// <inheritdoc />
public partial class Integrated_Price_For_Orders : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(
            name: "Price",
            schema: "Orders",
            table: "OngoingOrders",
            type: "numeric(19,2)",
            precision: 19,
            scale: 2,
            nullable: true);

        migrationBuilder.AddColumn<decimal>(
            name: "Price",
            schema: "Orders",
            table: "CompletedOrders",
            type: "numeric(19,2)",
            precision: 19,
            scale: 2,
            nullable: false,
            defaultValue: 0m);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Price",
            schema: "Orders",
            table: "OngoingOrders");

        migrationBuilder.DropColumn(
            name: "Price",
            schema: "Orders",
            table: "CompletedOrders");
    }
}
