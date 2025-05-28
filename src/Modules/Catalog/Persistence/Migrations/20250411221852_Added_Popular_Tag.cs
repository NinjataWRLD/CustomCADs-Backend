using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Catalog.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_Popular_Tag : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.InsertData(
			schema: "Catalog",
			table: "Tags",
			columns: ["Id", "Name"],
			values: [new Guid("9a35cbea-806c-4561-ae71-bb21824f2432"), "Popular"]);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DeleteData(
			schema: "Catalog",
			table: "Tags",
			keyColumn: "Id",
			keyValue: new Guid("9a35cbea-806c-4561-ae71-bb21824f2432"));
	}
}
