using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Orders.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Removed_Carts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GalleryOrders",
                schema: "Orders");

            migrationBuilder.DropTable(
                name: "Carts",
                schema: "Orders");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carts",
                schema: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BuyerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GalleryOrders",
                schema: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeliveryType = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_GalleryOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GalleryOrders_Carts_CartId",
                        column: x => x.CartId,
                        principalSchema: "Orders",
                        principalTable: "Carts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GalleryOrders_CartId",
                schema: "Orders",
                table: "GalleryOrders",
                column: "CartId");
        }
    }
}
