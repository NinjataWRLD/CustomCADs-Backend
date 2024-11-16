using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Catalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Fixed_Product_Mess : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CadPath",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CamX",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CamY",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CamZ",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PanX",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PanY",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PanZ",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "CadId",
                schema: "Catalog",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CadId",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "CadPath",
                schema: "Catalog",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CamX",
                schema: "Catalog",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CamY",
                schema: "Catalog",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CamZ",
                schema: "Catalog",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PanX",
                schema: "Catalog",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PanY",
                schema: "Catalog",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PanZ",
                schema: "Catalog",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
