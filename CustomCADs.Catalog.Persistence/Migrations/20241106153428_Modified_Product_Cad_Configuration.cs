using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Catalog.Persistence.Migrations;

/// <inheritdoc />
public partial class Modified_Product_Cad_Configuration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "PanCoordZ",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_PanCoordinates_Z");

        migrationBuilder.RenameColumn(
            name: "PanCoordY",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_PanCoordinates_Y");

        migrationBuilder.RenameColumn(
            name: "PanCoordX",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_PanCoordinates_X");

        migrationBuilder.RenameColumn(
            name: "CamCoordZ",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_CamCoordinates_Z");

        migrationBuilder.RenameColumn(
            name: "CamCoordY",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_CamCoordinates_Y");

        migrationBuilder.RenameColumn(
            name: "CamCoordX",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_CamCoordinates_X");

        migrationBuilder.RenameColumn(
            name: "CadPath",
            schema: "Catalog",
            table: "Products",
            newName: "Cad_Path");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "Cad_Path",
            schema: "Catalog",
            table: "Products",
            newName: "CadPath");

        migrationBuilder.RenameColumn(
            name: "Cad_PanCoordinates_Z",
            schema: "Catalog",
            table: "Products",
            newName: "PanCoordZ");

        migrationBuilder.RenameColumn(
            name: "Cad_PanCoordinates_Y",
            schema: "Catalog",
            table: "Products",
            newName: "PanCoordY");

        migrationBuilder.RenameColumn(
            name: "Cad_PanCoordinates_X",
            schema: "Catalog",
            table: "Products",
            newName: "PanCoordX");

        migrationBuilder.RenameColumn(
            name: "Cad_CamCoordinates_Z",
            schema: "Catalog",
            table: "Products",
            newName: "CamCoordZ");

        migrationBuilder.RenameColumn(
            name: "Cad_CamCoordinates_Y",
            schema: "Catalog",
            table: "Products",
            newName: "CamCoordY");

        migrationBuilder.RenameColumn(
            name: "Cad_CamCoordinates_X",
            schema: "Catalog",
            table: "Products",
            newName: "CamCoordX");
    }
}
