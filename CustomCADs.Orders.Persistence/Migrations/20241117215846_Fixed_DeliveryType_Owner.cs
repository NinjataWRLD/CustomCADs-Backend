using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Orders.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_DeliveryType_Owner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryType",
                schema: "Orders",
                table: "Carts");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryType",
                schema: "Orders",
                table: "GalleryOrders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryType",
                schema: "Orders",
                table: "GalleryOrders");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryType",
                schema: "Orders",
                table: "Carts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
