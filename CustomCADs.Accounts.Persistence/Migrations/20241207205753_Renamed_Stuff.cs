using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Accounts.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Renamed_Stuff : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users",
                schema: "Account");

            migrationBuilder.EnsureSchema(
                name: "Accounts");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Account",
                newName: "Roles",
                newSchema: "Accounts");

            migrationBuilder.CreateTable(
                name: "Accounts",
                schema: "Accounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(20)", nullable: false)
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

            migrationBuilder.EnsureSchema(
                name: "Account");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Accounts",
                newName: "Roles",
                newSchema: "Account");

            migrationBuilder.CreateTable(
                name: "Users",
                schema: "Account",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    TimeZone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleName",
                        column: x => x.RoleName,
                        principalSchema: "Account",
                        principalTable: "Roles",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "Account",
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "RoleName", "TimeZone", "Username" },
                values: new object[,]
                {
                    { new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"), "boriskolev2006@gmail.com", null, null, "Designer", "Sofia/Europe", "Oracle3000" },
                    { new Guid("2da61b05-1a27-4af9-9df2-be4f1f4e835f"), "ivanzlatinov006@gmail.com", null, null, "Client", "Sofia/Europe", "For7a7a" },
                    { new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"), "PDMatsaliev20@codingburgas.bg", null, null, "Contributor", "Sofia/Europe", "PetarDMatsaliev" },
                    { new Guid("e995039c-a535-4f20-8288-7aadcb71b252"), "ivanangelov414@gmail.com", null, null, "Administrator", "Sofia/Europe", "NinjataBG" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "Account",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleName",
                schema: "Account",
                table: "Users",
                column: "RoleName");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "Account",
                table: "Users",
                column: "Username",
                unique: true);
        }
    }
}
