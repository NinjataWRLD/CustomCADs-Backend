using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Accounts.Persistence.Migrations;

/// <inheritdoc />
public partial class Updated_Account_Data : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            schema: "Accounts",
            table: "Accounts",
            keyColumn: "Id",
            keyValue: new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"),
            column: "Username",
            value: "PDMatsaliev20");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            schema: "Accounts",
            table: "Accounts",
            keyColumn: "Id",
            keyValue: new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"),
            column: "Username",
            value: "PetarDMatsaliev");
    }
}
