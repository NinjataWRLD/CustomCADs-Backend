using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Account.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Added_Unique_Indexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                schema: "Account",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                schema: "Account",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Email",
                schema: "Account",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_Username",
                schema: "Account",
                table: "Users");
        }
    }
}
