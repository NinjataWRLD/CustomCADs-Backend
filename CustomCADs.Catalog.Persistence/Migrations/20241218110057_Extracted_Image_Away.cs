using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Catalog.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Extracted_Image_Away : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageContentType",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ImageKey",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "ImageId",
                schema: "Catalog",
                table: "Products",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                schema: "Catalog",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "ImageContentType",
                schema: "Catalog",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ImageKey",
                schema: "Catalog",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
