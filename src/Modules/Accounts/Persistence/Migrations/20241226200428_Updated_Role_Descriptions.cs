using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Accounts.Persistence.Migrations;

/// <inheritdoc />
public partial class Updated_Role_Descriptions : Migration
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
            value: "Can buy Products from the Gallery as Cart Items; Can make Orders to our Designers and contact them; Can download purchased CADs and track requested Shipments.");

        migrationBuilder.UpdateData(
            schema: "Accounts",
            table: "Roles",
            keyColumn: "Id",
            keyValue: 2,
            column: "Description",
            value: "Can upload 3D Models to the Gallery as Products; Can sell CADs to our Designers and contact them; Can apply to become a Designer himself.");

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

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            schema: "Accounts",
            table: "Roles",
            keyColumn: "Id",
            keyValue: 1,
            column: "Description",
            value: "Has access to Orders endpoints - can buy 3D Models from the Gallery and make and track Orders.");

        migrationBuilder.UpdateData(
            schema: "Accounts",
            table: "Roles",
            keyColumn: "Id",
            keyValue: 2,
            column: "Description",
            value: "Has access to Cads endpoints - can apply to upload his 3D Models to the Gallery, set their prices and track their status");

        migrationBuilder.UpdateData(
            schema: "Accounts",
            table: "Roles",
            keyColumn: "Id",
            keyValue: 3,
            column: "Description",
            value: "Has access to Cads and Designer endpoints - can upload his 3D Models straight to the Gallery, validate contributors' cads and finish clients' orders.");

        migrationBuilder.UpdateData(
            schema: "Accounts",
            table: "Roles",
            keyColumn: "Id",
            keyValue: 4,
            column: "Description",
            value: "Has full access to Users, Roles, Orders, Cads, Categories and all other endpoints - can do anyhting.");
    }
}
