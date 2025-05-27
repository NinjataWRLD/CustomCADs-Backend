using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Files.Persistence.Migrations;

/// <inheritdoc />
public partial class Changed_Coordinates_Type : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<decimal>(
			name: "PanZ",
			schema: "Files",
			table: "Cads",
			type: "numeric",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer");

		migrationBuilder.AlterColumn<decimal>(
			name: "PanY",
			schema: "Files",
			table: "Cads",
			type: "numeric",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer");

		migrationBuilder.AlterColumn<decimal>(
			name: "PanX",
			schema: "Files",
			table: "Cads",
			type: "numeric",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer");

		migrationBuilder.AlterColumn<decimal>(
			name: "CamZ",
			schema: "Files",
			table: "Cads",
			type: "numeric",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer");

		migrationBuilder.AlterColumn<decimal>(
			name: "CamY",
			schema: "Files",
			table: "Cads",
			type: "numeric",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer");

		migrationBuilder.AlterColumn<decimal>(
			name: "CamX",
			schema: "Files",
			table: "Cads",
			type: "numeric",
			nullable: false,
			oldClrType: typeof(int),
			oldType: "integer");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<int>(
			name: "PanZ",
			schema: "Files",
			table: "Cads",
			type: "integer",
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "numeric");

		migrationBuilder.AlterColumn<int>(
			name: "PanY",
			schema: "Files",
			table: "Cads",
			type: "integer",
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "numeric");

		migrationBuilder.AlterColumn<int>(
			name: "PanX",
			schema: "Files",
			table: "Cads",
			type: "integer",
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "numeric");

		migrationBuilder.AlterColumn<int>(
			name: "CamZ",
			schema: "Files",
			table: "Cads",
			type: "integer",
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "numeric");

		migrationBuilder.AlterColumn<int>(
			name: "CamY",
			schema: "Files",
			table: "Cads",
			type: "integer",
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "numeric");

		migrationBuilder.AlterColumn<int>(
			name: "CamX",
			schema: "Files",
			table: "Cads",
			type: "integer",
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "numeric");
	}
}
