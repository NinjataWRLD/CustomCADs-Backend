using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Accounts.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_Accounts_CreatedAt : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<DateTimeOffset>(
            name: "CreatedAt",
            schema: "Accounts",
            table: "Accounts",
            type: "timestamp with time zone",
            nullable: false,
            defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

        migrationBuilder.UpdateData(
            schema: "Accounts",
            table: "Accounts",
            keyColumn: "Id",
            keyValue: new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"),
            column: "CreatedAt",
            value: new DateTimeOffset(new DateTime(2025, 1, 9, 13, 15, 28, 789, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)));

        migrationBuilder.UpdateData(
            schema: "Accounts",
            table: "Accounts",
            keyColumn: "Id",
            keyValue: new Guid("2da61b05-1a27-4af9-9df2-be4f1f4e835f"),
            column: "CreatedAt",
            value: new DateTimeOffset(new DateTime(2025, 5, 10, 19, 23, 12, 123, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)));

        migrationBuilder.UpdateData(
            schema: "Accounts",
            table: "Accounts",
            keyColumn: "Id",
            keyValue: new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"),
            column: "CreatedAt",
            value: new DateTimeOffset(new DateTime(2025, 5, 13, 17, 42, 57, 456, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)));

        migrationBuilder.UpdateData(
            schema: "Accounts",
            table: "Accounts",
            keyColumn: "Id",
            keyValue: new Guid("e995039c-a535-4f20-8288-7aadcb71b252"),
            column: "CreatedAt",
            value: new DateTimeOffset(new DateTime(2024, 3, 17, 2, 45, 13, 0, DateTimeKind.Unspecified), new TimeSpan(0, 3, 0, 0, 0)));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedAt",
            schema: "Accounts",
            table: "Accounts");
    }
}
