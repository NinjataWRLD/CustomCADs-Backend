using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Accounts.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Accounts");

            migrationBuilder.CreateTable(
                name: "Roles",
                schema: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.UniqueConstraint("AK_Roles_Name", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(62)", maxLength: 62, nullable: false),
                    Email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    TimeZone = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(62)", maxLength: 62, nullable: true),
                    LastName = table.Column<string>(type: "character varying(62)", maxLength: 62, nullable: true),
                    RoleName = table.Column<string>(type: "character varying(20)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Roles_RoleName",
                        column: x => x.RoleName,
                        principalSchema: "Accounts",
                        principalTable: "Roles",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Accounts",
                table: "Roles",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Has access to Orders endpoints - can buy 3D Models from the Gallery and make and track Orders.", "Client" },
                    { 2, "Has access to Cads endpoints - can apply to upload his 3D Models to the Gallery, set their prices and track their status", "Contributor" },
                    { 3, "Has access to Cads and Designer endpoints - can upload his 3D Models straight to the Gallery, validate contributors' cads and finish clients' orders.", "Designer" },
                    { 4, "Has full access to Users, Roles, Orders, Cads, Categories and all other endpoints - can do anyhting.", "Administrator" }
                });

            migrationBuilder.InsertData(
                schema: "Accounts",
                table: "Accounts",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "RoleName", "TimeZone", "Username" },
                values: new object[,]
                {
                    { new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"), "boriskolev2006@gmail.com", null, null, "Designer", "Sofia/Europe", "Oracle3000" },
                    { new Guid("2da61b05-1a27-4af9-9df2-be4f1f4e835f"), "ivanzlatinov006@gmail.com", null, null, "Client", "Sofia/Europe", "For7a7a" },
                    { new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"), "PDMatsaliev20@codingburgas.bg", null, null, "Contributor", "Sofia/Europe", "PetarDMatsaliev" },
                    { new Guid("e995039c-a535-4f20-8288-7aadcb71b252"), "ivanangelov414@gmail.com", null, null, "Administrator", "Sofia/Europe", "NinjataBG" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Email",
                schema: "Accounts",
                table: "Accounts",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_RoleName",
                schema: "Accounts",
                table: "Accounts",
                column: "RoleName");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Username",
                schema: "Accounts",
                table: "Accounts",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts",
                schema: "Accounts");

            migrationBuilder.DropTable(
                name: "Roles",
                schema: "Accounts");
        }
    }
}
