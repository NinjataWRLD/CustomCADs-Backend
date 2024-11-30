using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Accounts.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Seeded_Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Account",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"));

            migrationBuilder.DeleteData(
                schema: "Account",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("2da61b05-1a27-4af9-9df2-be4f1f4e835f"));

            migrationBuilder.DeleteData(
                schema: "Account",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"));

            migrationBuilder.DeleteData(
                schema: "Account",
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e995039c-a535-4f20-8288-7aadcb71b252"));
        }
    }
}
