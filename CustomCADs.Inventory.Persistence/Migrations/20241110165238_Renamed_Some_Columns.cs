using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Inventory.Persistence.Migrations;

/// <inheritdoc />
public partial class Renamed_Some_Columns : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "ImagePath",
            schema: "Catalog",
            table: "Products",
            newName: "Price_Symbol");

        migrationBuilder.RenameColumn(
            name: "Cost",
            schema: "Catalog",
            table: "Products",
            newName: "Price_Amount");

        migrationBuilder.AddColumn<string>(
            name: "Image_Path",
            schema: "Catalog",
            table: "Products",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<string>(
            name: "Price_Currency",
            schema: "Catalog",
            table: "Products",
            type: "nvarchar(max)",
            nullable: false,
            defaultValue: "");

        migrationBuilder.AddColumn<int>(
            name: "Price_Precision",
            schema: "Catalog",
            table: "Products",
            type: "int",
            nullable: false,
            defaultValue: 0);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Image_Path",
            schema: "Catalog",
            table: "Products");

        migrationBuilder.DropColumn(
            name: "Price_Currency",
            schema: "Catalog",
            table: "Products");

        migrationBuilder.DropColumn(
            name: "Price_Precision",
            schema: "Catalog",
            table: "Products");

        migrationBuilder.RenameColumn(
            name: "Price_Symbol",
            schema: "Catalog",
            table: "Products",
            newName: "ImagePath");

        migrationBuilder.RenameColumn(
            name: "Price_Amount",
            schema: "Catalog",
            table: "Products",
            newName: "Cost");
    }
}
