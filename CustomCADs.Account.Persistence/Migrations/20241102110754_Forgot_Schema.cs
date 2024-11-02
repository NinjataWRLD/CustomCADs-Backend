using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Account.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Forgot_Schema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Account");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "Users",
                newSchema: "Account");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Roles",
                newSchema: "Account");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Users",
                schema: "Account",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "Roles",
                schema: "Account",
                newName: "Roles");
        }
    }
}
