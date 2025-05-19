using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Catalog.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial_Migration : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			name: "Catalog");

		migrationBuilder.CreateTable(
			name: "Products",
			schema: "Catalog",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uuid", nullable: false),
				Name = table.Column<string>(type: "character varying(18)", maxLength: 18, nullable: false),
				Description = table.Column<string>(type: "character varying(750)", maxLength: 750, nullable: false),
				Price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
				Status = table.Column<string>(type: "text", nullable: false),
				UploadedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
				CategoryId = table.Column<int>(type: "integer", nullable: false),
				ImageId = table.Column<Guid>(type: "uuid", nullable: false),
				CadId = table.Column<Guid>(type: "uuid", nullable: false),
				CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
				DesignerId = table.Column<Guid>(type: "uuid", nullable: true),
				Purchases = table.Column<int>(type: "integer", nullable: false),
				Views = table.Column<int>(type: "integer", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Products", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "Tags",
			schema: "Catalog",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uuid", nullable: false),
				Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Tags", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "ProductTags",
			schema: "Catalog",
			columns: table => new
			{
				ProductId = table.Column<Guid>(type: "uuid", nullable: false),
				TagId = table.Column<Guid>(type: "uuid", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_ProductTags", x => new { x.ProductId, x.TagId });
				table.ForeignKey(
					name: "FK_ProductTags_Products_ProductId",
					column: x => x.ProductId,
					principalSchema: "Catalog",
					principalTable: "Products",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
				table.ForeignKey(
					name: "FK_ProductTags_Tags_TagId",
					column: x => x.TagId,
					principalSchema: "Catalog",
					principalTable: "Tags",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.InsertData(
			schema: "Catalog",
			table: "Tags",
			columns: ["Id", "Name"],
			values: new object[,]
			{
				{ new Guid("38deab9b-8791-4147-9958-64e9f7ec6d78"), "Printable" },
				{ new Guid("6813c4b9-bcde-4f95-a1ce-8e545756c8a4"), "New" },
				{ new Guid("e67f88d5-330a-414d-b45d-32c6806725ab"), "Professional" }
			});

		migrationBuilder.CreateIndex(
			name: "IX_ProductTags_TagId",
			schema: "Catalog",
			table: "ProductTags",
			column: "TagId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "ProductTags",
			schema: "Catalog");

		migrationBuilder.DropTable(
			name: "Products",
			schema: "Catalog");

		migrationBuilder.DropTable(
			name: "Tags",
			schema: "Catalog");
	}
}
