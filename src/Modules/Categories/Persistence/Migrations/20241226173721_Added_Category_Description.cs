using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Categories.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_Category_Description : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<string>(
			name: "Description",
			schema: "Categories",
			table: "Categories",
			type: "character varying(1000)",
			maxLength: 1000,
			nullable: false,
			defaultValue: "");

		migrationBuilder.UpdateData(
			schema: "Categories",
			table: "Categories",
			keyColumn: "Id",
			keyValue: 1,
			column: "Description",
			value: "Includes pets, wild animals, etc.");

		migrationBuilder.UpdateData(
			schema: "Categories",
			table: "Categories",
			keyColumn: "Id",
			keyValue: 2,
			column: "Description",
			value: "Includes movie characters, book characters, game characters, etc.");

		migrationBuilder.UpdateData(
			schema: "Categories",
			table: "Categories",
			keyColumn: "Id",
			keyValue: 3,
			column: "Description",
			value: "Includes phones, computers, e-devices, earphones, etc.");

		migrationBuilder.UpdateData(
			schema: "Categories",
			table: "Categories",
			keyColumn: "Id",
			keyValue: 4,
			column: "Description",
			value: "Includes clothes, shoes, accessories, hats, etc.");

		migrationBuilder.UpdateData(
			schema: "Categories",
			table: "Categories",
			keyColumn: "Id",
			keyValue: 5,
			column: "Description",
			value: "Includes tables, chairs, beds, etc.");

		migrationBuilder.UpdateData(
			schema: "Categories",
			table: "Categories",
			keyColumn: "Id",
			keyValue: 6,
			column: "Description",
			value: "Includes flowers, forests, seas, etc.");

		migrationBuilder.UpdateData(
			schema: "Categories",
			table: "Categories",
			keyColumn: "Id",
			keyValue: 7,
			column: "Description",
			value: "Includes organs, tools, chemical fluids, etc.");

		migrationBuilder.UpdateData(
			schema: "Categories",
			table: "Categories",
			keyColumn: "Id",
			keyValue: 8,
			column: "Description",
			value: "Includes footballs, boxing gloves, hockey sticks, etc.");

		migrationBuilder.UpdateData(
			schema: "Categories",
			table: "Categories",
			keyColumn: "Id",
			keyValue: 9,
			column: "Description",
			value: "Includes pet toys, action figures, plushies, etc.");

		migrationBuilder.UpdateData(
			schema: "Categories",
			table: "Categories",
			keyColumn: "Id",
			keyValue: 10,
			column: "Description",
			value: "Includes cars, trucks, tanks, bikes, planes, ships, etc.");

		migrationBuilder.UpdateData(
			schema: "Categories",
			table: "Categories",
			keyColumn: "Id",
			keyValue: 11,
			column: "Description",
			value: "Includes anything that doesn't fit into the other categories.");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "Description",
			schema: "Categories",
			table: "Categories");
	}
}
