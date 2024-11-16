using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Orders.Persistence.Migrations;

/// <inheritdoc />
public partial class Extracted_Cad_And_Shipment : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Cads",
            schema: "Orders");

        migrationBuilder.DropTable(
            name: "Shipments",
            schema: "Orders");

        migrationBuilder.AddColumn<Guid>(
            name: "CadId",
            schema: "Orders",
            table: "GalleryOrderItems",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "ShipmentId",
            schema: "Orders",
            table: "GalleryOrderItems",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "CadId",
            schema: "Orders",
            table: "CustomOrders",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "ShipmentId",
            schema: "Orders",
            table: "CustomOrders",
            type: "uniqueidentifier",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CadId",
            schema: "Orders",
            table: "GalleryOrderItems");

        migrationBuilder.DropColumn(
            name: "ShipmentId",
            schema: "Orders",
            table: "GalleryOrderItems");

        migrationBuilder.DropColumn(
            name: "CadId",
            schema: "Orders",
            table: "CustomOrders");

        migrationBuilder.DropColumn(
            name: "ShipmentId",
            schema: "Orders",
            table: "CustomOrders");

        migrationBuilder.CreateTable(
            name: "Cads",
            schema: "Orders",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CamX = table.Column<int>(type: "int", nullable: false),
                CamY = table.Column<int>(type: "int", nullable: false),
                CamZ = table.Column<int>(type: "int", nullable: false),
                PanX = table.Column<int>(type: "int", nullable: false),
                PanY = table.Column<int>(type: "int", nullable: false),
                PanZ = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Cads", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Shipments",
            schema: "Orders",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Street = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Shipments", x => x.Id);
            });
    }
}
