using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations;

/// <inheritdoc />
public partial class Removed_ActiveCart : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_ActiveCartItems_ActiveCarts_CartId",
            schema: "Carts",
            table: "ActiveCartItems");

        migrationBuilder.DropTable(
            name: "ActiveCarts",
            schema: "Carts");

        migrationBuilder.DropPrimaryKey(
            name: "PK_ActiveCartItems",
            schema: "Carts",
            table: "ActiveCartItems");

        migrationBuilder.DropIndex(
            name: "IX_ActiveCartItems_CartId",
            schema: "Carts",
            table: "ActiveCartItems");

        migrationBuilder.RenameColumn(
            name: "CartId",
            schema: "Carts",
            table: "ActiveCartItems",
            newName: "BuyerId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_ActiveCartItems",
            schema: "Carts",
            table: "ActiveCartItems",
            columns: new[] { "ProductId", "BuyerId" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropPrimaryKey(
            name: "PK_ActiveCartItems",
            schema: "Carts",
            table: "ActiveCartItems");

        migrationBuilder.RenameColumn(
            name: "BuyerId",
            schema: "Carts",
            table: "ActiveCartItems",
            newName: "CartId");

        migrationBuilder.AddPrimaryKey(
            name: "PK_ActiveCartItems",
            schema: "Carts",
            table: "ActiveCartItems",
            column: "ProductId");

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

        migrationBuilder.CreateIndex(
            name: "IX_ActiveCartItems_CartId",
            schema: "Carts",
            table: "ActiveCartItems",
            column: "CartId");

        migrationBuilder.AddForeignKey(
            name: "FK_ActiveCartItems_ActiveCarts_CartId",
            schema: "Carts",
            table: "ActiveCartItems",
            column: "CartId",
            principalSchema: "Carts",
            principalTable: "ActiveCarts",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }
}
