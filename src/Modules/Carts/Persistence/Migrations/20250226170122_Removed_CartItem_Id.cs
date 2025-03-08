using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations;

/// <inheritdoc />
public partial class Removed_CartItem_Id : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_PurchasedCartItems",
            schema: "Carts",
            table: "PurchasedCartItems");

        migrationBuilder.DropPrimaryKey(
            name: "PK_ActiveCartItems",
            schema: "Carts",
            table: "ActiveCartItems");

        migrationBuilder.DropColumn(
            name: "Id",
            schema: "Carts",
            table: "PurchasedCartItems");

        migrationBuilder.DropColumn(
            name: "Id",
            schema: "Carts",
            table: "ActiveCartItems");

        migrationBuilder.AddPrimaryKey(
            name: "PK_PurchasedCartItems",
            schema: "Carts",
            table: "PurchasedCartItems",
            column: "ProductId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_ActiveCartItems",
            schema: "Carts",
            table: "ActiveCartItems",
            column: "ProductId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_PurchasedCartItems",
            schema: "Carts",
            table: "PurchasedCartItems");

        migrationBuilder.DropPrimaryKey(
            name: "PK_ActiveCartItems",
            schema: "Carts",
            table: "ActiveCartItems");

        migrationBuilder.AddColumn<Guid>(
            name: "Id",
            schema: "Carts",
            table: "PurchasedCartItems",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddColumn<Guid>(
            name: "Id",
            schema: "Carts",
            table: "ActiveCartItems",
            type: "uuid",
            nullable: false,
            defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

        migrationBuilder.AddPrimaryKey(
            name: "PK_PurchasedCartItems",
            schema: "Carts",
            table: "PurchasedCartItems",
            column: "Id");

        migrationBuilder.AddPrimaryKey(
            name: "PK_ActiveCartItems",
            schema: "Carts",
            table: "ActiveCartItems",
            column: "Id");
    }
}
