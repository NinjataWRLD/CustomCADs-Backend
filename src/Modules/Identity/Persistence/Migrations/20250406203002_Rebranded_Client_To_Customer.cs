using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Identity.Persistence.Migrations;

/// <inheritdoc />
public partial class Rebranded_Client_To_Customer : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("762ddec2-25c9-4183-9891-72a19d84a839"),
            columns: new[] { "Name", "NormalizedName" },
            values: new object[] { "Customer", "CUSTOMER" });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetRoles",
            keyColumn: "Id",
            keyValue: new Guid("762ddec2-25c9-4183-9891-72a19d84a839"),
            columns: new[] { "Name", "NormalizedName" },
            values: new object[] { "Client", "CLIENT" });
    }
}
