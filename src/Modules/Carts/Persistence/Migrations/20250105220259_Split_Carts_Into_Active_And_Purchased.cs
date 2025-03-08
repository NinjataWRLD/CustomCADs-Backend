using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations;

/// <inheritdoc />
public partial class Split_Carts_Into_Active_And_Purchased : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CartItems",
            schema: "Carts");

        migrationBuilder.DropTable(
            name: "Carts",
            schema: "Carts");

        migrationBuilder.CreateTable(
            name: "ActiveCarts",
            schema: "Carts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                BuyerId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ActiveCarts", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "PurchasedCarts",
            schema: "Carts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                PurchaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                ShipmentId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PurchasedCarts", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "ActiveCartItems",
            schema: "Carts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Quantity = table.Column<int>(type: "integer", nullable: false),
                Weight = table.Column<double>(type: "double precision", precision: 6, scale: 2, nullable: false),
                Delivery = table.Column<bool>(type: "boolean", nullable: false),
                ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                CartId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ActiveCartItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_ActiveCartItems_ActiveCarts_CartId",
                    column: x => x.CartId,
                    principalSchema: "Carts",
                    principalTable: "ActiveCarts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "PurchasedCartItems",
            schema: "Carts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Quantity = table.Column<int>(type: "integer", nullable: false),
                Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                Delivery = table.Column<bool>(type: "boolean", nullable: false),
                ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                CartId = table.Column<Guid>(type: "uuid", nullable: false),
                CadId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PurchasedCartItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_PurchasedCartItems_PurchasedCarts_CartId",
                    column: x => x.CartId,
                    principalSchema: "Carts",
                    principalTable: "PurchasedCarts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_ActiveCartItems_CartId",
            schema: "Carts",
            table: "ActiveCartItems",
            column: "CartId");

        migrationBuilder.CreateIndex(
            name: "IX_PurchasedCartItems_CartId",
            schema: "Carts",
            table: "PurchasedCartItems",
            column: "CartId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "ActiveCartItems",
            schema: "Carts");

        migrationBuilder.DropTable(
            name: "PurchasedCartItems",
            schema: "Carts");

        migrationBuilder.DropTable(
            name: "ActiveCarts",
            schema: "Carts");

        migrationBuilder.DropTable(
            name: "PurchasedCarts",
            schema: "Carts");

        migrationBuilder.CreateTable(
            name: "Carts",
            schema: "Carts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                PurchaseDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                ShipmentId = table.Column<Guid>(type: "uuid", nullable: true),
                Status = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Carts", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "CartItems",
            schema: "Carts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                CartId = table.Column<Guid>(type: "uuid", nullable: false),
                CadId = table.Column<Guid>(type: "uuid", nullable: true),
                Delivery = table.Column<bool>(type: "boolean", nullable: false),
                Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                Quantity = table.Column<int>(type: "integer", nullable: false),
                Weight = table.Column<double>(type: "double precision", precision: 6, scale: 2, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CartItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_CartItems_Carts_CartId",
                    column: x => x.CartId,
                    principalSchema: "Carts",
                    principalTable: "Carts",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_CartItems_CartId",
            schema: "Carts",
            table: "CartItems",
            column: "CartId");
    }
}
