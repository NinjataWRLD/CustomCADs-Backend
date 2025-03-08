using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Files.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_Cad_Volume : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<decimal>(
            name: "Volume",
            schema: "Files",
            table: "Cads",
            type: "numeric",
            nullable: false,
            defaultValue: 0m);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Volume",
            schema: "Files",
            table: "Cads");
    }
}
