using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations;

/// <inheritdoc />
public partial class Fixed_PurchasedCartItems_PK : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_PurchasedCartItems",
            schema: "Carts",
            table: "PurchasedCartItems");

        migrationBuilder.AddPrimaryKey(
            name: "PK_PurchasedCartItems",
            schema: "Carts",
            table: "PurchasedCartItems",
            columns: new[] { "ProductId", "CartId" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_PurchasedCartItems",
            schema: "Carts",
            table: "PurchasedCartItems");

        migrationBuilder.AddPrimaryKey(
            name: "PK_PurchasedCartItems",
            schema: "Carts",
            table: "PurchasedCartItems",
            column: "ProductId");
    }
}
