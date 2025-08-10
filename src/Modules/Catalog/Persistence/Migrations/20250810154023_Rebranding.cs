using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CustomCADs.Catalog.Persistence.Migrations;

/// <inheritdoc />
public partial class Rebranding : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.CreateTable(
			name: "Categories",
			schema: "Catalog",
			columns: table => new
			{
				Id = table.Column<int>(type: "integer", nullable: false)
					.Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
				Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
				Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Categories", x => x.Id);
			});

		migrationBuilder.InsertData(
			schema: "Catalog",
			table: "Categories",
			columns: new[] { "Id", "Description", "Name" },
			values: new object[,]
			{
					{ 1, "Includes pets, wild animals, etc.", "Animals" },
					{ 2, "Includes movie characters, book characters, game characters, etc.", "Characters" },
					{ 3, "Includes phones, computers, e-devices, earphones, etc.", "Electronics" },
					{ 4, "Includes clothes, shoes, accessories, hats, etc.", "Fashion" },
					{ 5, "Includes tables, chairs, beds, etc.", "Furniture" },
					{ 6, "Includes flowers, forests, seas, etc.", "Nature" },
					{ 7, "Includes organs, tools, chemical fluids, etc.", "Science" },
					{ 8, "Includes footballs, boxing gloves, hockey sticks, etc.", "Sports" },
					{ 9, "Includes pet toys, action figures, plushies, etc.", "Toys" },
					{ 10, "Includes cars, trucks, tanks, bikes, planes, ships, etc.", "Vehicles" },
					{ 11, "Includes anything that doesn't fit into the other categories.", "Others" }
			});
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "Categories",
			schema: "Catalog");
	}
}
