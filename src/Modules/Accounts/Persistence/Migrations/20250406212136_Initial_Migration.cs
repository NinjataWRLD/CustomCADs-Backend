using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Accounts.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial_Migration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Accounts");

        migrationBuilder.CreateTable(
            name: "Accounts",
            schema: "Accounts",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Username = table.Column<string>(type: "character varying(62)", maxLength: 62, nullable: false),
                Email = table.Column<string>(type: "text", nullable: false),
                TimeZone = table.Column<string>(type: "text", nullable: false),
                FirstName = table.Column<string>(type: "character varying(62)", maxLength: 62, nullable: true),
                LastName = table.Column<string>(type: "character varying(62)", maxLength: 62, nullable: true),
                RoleName = table.Column<string>(type: "text", nullable: false),
                ViewedProductIds = table.Column<Guid[]>(type: "uuid[]", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Accounts", x => x.Id);
            });

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
            });

        migrationBuilder.InsertData(
            schema: "Accounts",
            table: "Accounts",
            columns: new[] { "Id", "Email", "FirstName", "LastName", "RoleName", "TimeZone", "Username", "ViewedProductIds" },
            values: new object[,]
            {
                { new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"), "boriskolev2006@gmail.com", null, null, "Designer", "Europe/Sofia", "Oracle3000", new Guid[0] },
                { new Guid("2da61b05-1a27-4af9-9df2-be4f1f4e835f"), "ivanzlatinov006@gmail.com", null, null, "Customer", "Europe/Sofia", "For7a7a", new Guid[0] },
                { new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"), "PDMatsaliev20@codingburgas.bg", null, null, "Contributor", "Europe/Sofia", "PDMatsaliev20", new Guid[0] },
                { new Guid("e995039c-a535-4f20-8288-7aadcb71b252"), "ivanangelov414@gmail.com", null, null, "Administrator", "Europe/Sofia", "NinjataBG", new Guid[0] }
            });

        migrationBuilder.InsertData(
            schema: "Accounts",
            table: "Roles",
            columns: new[] { "Id", "Description", "Name" },
            values: new object[,]
            {
                { 1, "Can buy Products from the Gallery as Cart Items; Can request Customs from our Designers and contact them; Can download purchased CADs and track requested Shipments.", "Customer" },
                { 2, "Can upload 3D Models to the Gallery as Products; Can sell CADs to our Designers and contact them; Can apply to become a Designer himself.", "Contributor" },
                { 3, "Can accept and work on Customers' Customs; Can validate or report Contributors' Products; Can do everything a Contributor can do.", "Designer" },
                { 4, "Can access all non-sensitive info from all resources; Can ban reported resources - Customs, Products, Users, ...; Can modify Categories and Roles.", "Administrator" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Accounts_Email",
            schema: "Accounts",
            table: "Accounts",
            column: "Email",
            unique: true);

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
