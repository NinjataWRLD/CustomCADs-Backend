using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Printing.Persistence.Migrations;

/// <inheritdoc />
public partial class Rebranding : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			name: "Printing");

		migrationBuilder.RenameTable(
			name: "Materials",
			schema: "Customizations",
			newName: "Materials",
			newSchema: "Printing");

		migrationBuilder.RenameTable(
			name: "Customizations",
			schema: "Customizations",
			newName: "Customizations",
			newSchema: "Printing");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			name: "Customizations");

		migrationBuilder.RenameTable(
			name: "Materials",
			schema: "Printing",
			newName: "Materials",
			newSchema: "Customizations");

		migrationBuilder.RenameTable(
			name: "Customizations",
			schema: "Printing",
			newName: "Customizations",
			newSchema: "Customizations");
	}
}
