using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Customizations.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Customizations");

            migrationBuilder.CreateTable(
                name: "Customizations",
                schema: "Customizations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Scale = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: false),
                    Infill = table.Column<decimal>(type: "numeric(4,2)", precision: 4, scale: 2, nullable: false),
                    Volume = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Color = table.Column<string>(type: "character varying(7)", maxLength: 7, nullable: false),
                    MaterialId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customizations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                schema: "Customizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Density = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    Cost = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    TextureId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "Customizations",
                table: "Materials",
                columns: new[] { "Id", "Cost", "Density", "Name", "TextureId" },
                values: new object[,]
                {
                    { 1, 30m, 1.24m, "PLA", new Guid("9a35cbea-806c-4561-ae71-bb21824f2432") },
                    { 2, 30m, 1.04m, "ABS", new Guid("bed27a31-107a-4b3f-a50a-cb9cc6f376f1") },
                    { 3, 30m, 1.25m, "Glow in dark", new Guid("190a69a3-1b02-43f0-a4f9-cab22826abf3") },
                    { 4, 30m, 1.27m, "TUF", new Guid("38deab9b-8791-4147-9958-64e9f7ec6d78") },
                    { 5, 30m, 1.23m, "Wood", new Guid("3fe2472c-d2c6-434c-a013-ef117319bed3") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customizations",
                schema: "Customizations");

            migrationBuilder.DropTable(
                name: "Materials",
                schema: "Customizations");
        }
    }
}
