using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Files.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_Image_Seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Files",
                table: "Images",
                columns: new[] { "Id", "ContentType", "Key" },
                values: new object[,]
                {
                    { new Guid("190a69a3-1b02-43f0-a4f9-cab22826abf3"), "image/webp", "textures/glow-in-dark.webp" },
                    { new Guid("38deab9b-8791-4147-9958-64e9f7ec6d78"), "image/webp", "textures/tuf.webp" },
                    { new Guid("3fe2472c-d2c6-434c-a013-ef117319bed3"), "image/webp", "textures/wood.webp" },
                    { new Guid("9a35cbea-806c-4561-ae71-bb21824f2432"), "image/webp", "textures/pla.webp" },
                    { new Guid("bed27a31-107a-4b3f-a50a-cb9cc6f376f1"), "image/webp", "textures/abs.webp" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Files",
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("190a69a3-1b02-43f0-a4f9-cab22826abf3"));

            migrationBuilder.DeleteData(
                schema: "Files",
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("38deab9b-8791-4147-9958-64e9f7ec6d78"));

            migrationBuilder.DeleteData(
                schema: "Files",
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("3fe2472c-d2c6-434c-a013-ef117319bed3"));

            migrationBuilder.DeleteData(
                schema: "Files",
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("9a35cbea-806c-4561-ae71-bb21824f2432"));

            migrationBuilder.DeleteData(
                schema: "Files",
                table: "Images",
                keyColumn: "Id",
                keyValue: new Guid("bed27a31-107a-4b3f-a50a-cb9cc6f376f1"));
        }
    }
}
