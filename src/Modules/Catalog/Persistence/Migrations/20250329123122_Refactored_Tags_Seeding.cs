using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Catalog.Persistence.Migrations;

/// <inheritdoc />
public partial class Refactored_Tags_Seeding : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "Catalog",
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("5957f822-77a3-4a72-964d-bf7740e994a5"));

        migrationBuilder.InsertData(
            schema: "Catalog",
            table: "Tags",
            columns: new[] { "Id", "Name" },
            values: new object[] { new Guid("38deab9b-8791-4147-9958-64e9f7ec6d78"), "Printable" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            schema: "Catalog",
            table: "Tags",
            keyColumn: "Id",
            keyValue: new Guid("38deab9b-8791-4147-9958-64e9f7ec6d78"));

        migrationBuilder.InsertData(
            schema: "Catalog",
            table: "Tags",
            columns: new[] { "Id", "Name" },
            values: new object[] { new Guid("5957f822-77a3-4a72-964d-bf7740e994a5"), "Popular" });
    }
}
