using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Catalog.Persistence.Migrations;

/// <inheritdoc />
public partial class Renamed_Some_More_Columns : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Price_Symbol",
            schema: "Catalog",
            table: "Products",
            newName: "PriceSymbol");

        migrationBuilder.RenameColumn(
            name: "Price_Precision",
            schema: "Catalog",
            table: "Products",
            newName: "PricePrecision");

        migrationBuilder.RenameColumn(
            name: "Price_Currency",
            schema: "Catalog",
            table: "Products",
            newName: "PriceCurrency");

        migrationBuilder.RenameColumn(
            name: "Price_Amount",
            schema: "Catalog",
            table: "Products",
            newName: "PriceAmount");

        migrationBuilder.RenameColumn(
            name: "Image_Path",
            schema: "Catalog",
            table: "Products",
            newName: "ImagePath");

        migrationBuilder.RenameColumn(
            name: "Cad_Path",
            schema: "Catalog",
            table: "Products",
            newName: "CadPath");

        migrationBuilder.RenameColumn(
            name: "Cad_PanCoordinates_Z",
            schema: "Catalog",
            table: "Products",
            newName: "PanZ");

        migrationBuilder.RenameColumn(
            name: "Cad_PanCoordinates_Y",
            schema: "Catalog",
            table: "Products",
            newName: "PanY");

        migrationBuilder.RenameColumn(
            name: "Cad_PanCoordinates_X",
            schema: "Catalog",
            table: "Products",
            newName: "PanX");

        migrationBuilder.RenameColumn(
            name: "Cad_CamCoordinates_Z",
            schema: "Catalog",
            table: "Products",
            newName: "CamZ");

        migrationBuilder.RenameColumn(
            name: "Cad_CamCoordinates_Y",
            schema: "Catalog",
            table: "Products",
            newName: "CamY");

        migrationBuilder.RenameColumn(
            name: "Cad_CamCoordinates_X",
            schema: "Catalog",
            table: "Products",
            newName: "CamX");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "PriceSymbol",
            schema: "Catalog",
            table: "Products",
            newName: "Price_Symbol");

        migrationBuilder.RenameColumn(
            name: "PricePrecision",
            schema: "Catalog",
            table: "Products",
            newName: "Price_Precision");

        migrationBuilder.RenameColumn(
            name: "PriceCurrency",
            schema: "Catalog",
            table: "Products",
            newName: "Price_Currency");

        migrationBuilder.RenameColumn(
            name: "PriceAmount",
            schema: "Catalog",
            table: "Products",
            newName: "Price_Amount");

        migrationBuilder.RenameColumn(
            name: "PanZ",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_PanCoordinates_Z");

        migrationBuilder.RenameColumn(
            name: "PanY",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_PanCoordinates_Y");

        migrationBuilder.RenameColumn(
            name: "PanX",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_PanCoordinates_X");

        migrationBuilder.RenameColumn(
            name: "ImagePath",
            schema: "Catalog",
            table: "Products",
            newName: "Image_Path");

        migrationBuilder.RenameColumn(
            name: "CamZ",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_CamCoordinates_Z");

        migrationBuilder.RenameColumn(
            name: "CamY",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_CamCoordinates_Y");

        migrationBuilder.RenameColumn(
            name: "CamX",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_CamCoordinates_X");

        migrationBuilder.RenameColumn(
            name: "CadPath",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_Path");
    }
}
