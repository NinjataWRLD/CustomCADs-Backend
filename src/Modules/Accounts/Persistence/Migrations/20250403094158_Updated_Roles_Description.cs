using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Accounts.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Roles_Description : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Accounts",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Can buy Products from the Gallery as Cart Items; Can request Customs from our Designers and contact them; Can download purchased CADs and track requested Shipments.");

            migrationBuilder.UpdateData(
                schema: "Accounts",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Can accept and work on Clients' Customs; Can validate or report Contributors' Products; Can do everything a Contributor can do.");

            migrationBuilder.UpdateData(
                schema: "Accounts",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Can access all non-sensitive info from all resources; Can ban reported resources - Customs, Products, Users, ...; Can modify Categories and Roles.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "Accounts",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Can buy Products from the Gallery as Cart Items; Can make Orders to our Designers and contact them; Can download purchased CADs and track requested Shipments.");

            migrationBuilder.UpdateData(
                schema: "Accounts",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                column: "Description",
                value: "Can accept and work on Clients' Orders; Can validate or report Contributors' Products; Can do everything a Contributor can do.");

            migrationBuilder.UpdateData(
                schema: "Accounts",
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                column: "Description",
                value: "Can access all non-sensitive info from all resources; Can ban reported resources - Orders, Products, Users, ...; Can modify Categories and Roles.");
        }
    }
}
