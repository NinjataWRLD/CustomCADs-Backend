using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Orders.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Renamed_Stuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryOrderItems",
                schema: "Orders");

            migrationBuilder.RenameColumn(
                name: "Total",
                schema: "Orders",
                table: "GalleryOrders",
                newName: "PriceAmount");

            migrationBuilder.RenameColumn(
                name: "DeliveryType",
                schema: "Orders",
                table: "GalleryOrders",
                newName: "PriceSymbol");

            migrationBuilder.RenameColumn(
                name: "BuyerId",
                schema: "Orders",
                table: "GalleryOrders",
                newName: "ProductId");

            migrationBuilder.AddColumn<Guid>(
                name: "CadId",
                schema: "Orders",
                table: "GalleryOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CartId",
                schema: "Orders",
                table: "GalleryOrders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "PriceCurrency",
                schema: "Orders",
                table: "GalleryOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PricePrecision",
                schema: "Orders",
                table: "GalleryOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                schema: "Orders",
                table: "GalleryOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "ShipmentId",
                schema: "Orders",
                table: "GalleryOrders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DeliveryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryOrders_CartId",
                schema: "Orders",
                table: "GalleryOrders",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_GalleryOrders_Carts_CartId",
                schema: "Orders",
                table: "GalleryOrders",
                column: "CartId",
                principalSchema: "Orders",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GalleryOrders_Carts_CartId",
                schema: "Orders",
                table: "GalleryOrders");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_GalleryOrders_CartId",
                schema: "Orders",
                table: "GalleryOrders");

            migrationBuilder.DropColumn(
                name: "CadId",
                schema: "Orders",
                table: "GalleryOrders");

            migrationBuilder.DropColumn(
                name: "CartId",
                schema: "Orders",
                table: "GalleryOrders");

            migrationBuilder.DropColumn(
                name: "PriceCurrency",
                schema: "Orders",
                table: "GalleryOrders");

            migrationBuilder.DropColumn(
                name: "PricePrecision",
                schema: "Orders",
                table: "GalleryOrders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "Orders",
                table: "GalleryOrders");

            migrationBuilder.DropColumn(
                name: "ShipmentId",
                schema: "Orders",
                table: "GalleryOrders");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                schema: "Orders",
                table: "GalleryOrders",
                newName: "BuyerId");

            migrationBuilder.RenameColumn(
                name: "PriceSymbol",
                schema: "Orders",
                table: "GalleryOrders",
                newName: "DeliveryType");

            migrationBuilder.RenameColumn(
                name: "PriceAmount",
                schema: "Orders",
                table: "GalleryOrders",
                newName: "Total");

            migrationBuilder.CreateTable(
                name: "GalleryOrderItems",
                schema: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GalleryOrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    ShipmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PriceAmount = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    PriceCurrency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PricePrecision = table.Column<int>(type: "int", nullable: false),
                    PriceSymbol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GalleryOrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryOrderItems_GalleryOrders_GalleryOrderId",
                        column: x => x.GalleryOrderId,
                        principalSchema: "Orders",
                        principalTable: "GalleryOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryOrderItems_GalleryOrderId",
                schema: "Orders",
                table: "GalleryOrderItems",
                column: "GalleryOrderId");
        }
    }
}
