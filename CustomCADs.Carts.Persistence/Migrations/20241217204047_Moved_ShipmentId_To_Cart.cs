using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Moved_ShipmentId_To_Cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipmentId",
                schema: "Carts",
                table: "CartItems");

            migrationBuilder.AddColumn<Guid>(
                name: "ShipmentId",
                schema: "Carts",
                table: "Carts",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipmentId",
                schema: "Carts",
                table: "Carts");

            migrationBuilder.AddColumn<Guid>(
                name: "ShipmentId",
                schema: "Carts",
                table: "CartItems",
                type: "uuid",
                nullable: true);
        }
    }
}
