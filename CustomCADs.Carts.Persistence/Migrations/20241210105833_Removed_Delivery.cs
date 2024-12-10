using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Removed_Delivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Delivery",
                schema: "Carts",
                table: "CartItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Delivery",
                schema: "Carts",
                table: "CartItems",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
