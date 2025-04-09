using CustomCADs.Identity.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Identity.Persistence.Migrations;

/// <inheritdoc />
public partial class Improved_AppUser_RefreshToken : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "RefreshToken",
            schema: "Identity",
            table: "AspNetUsers");

        migrationBuilder.DropColumn(
            name: "RefreshTokenEndDate",
            schema: "Identity",
            table: "AspNetUsers");

        migrationBuilder.AddColumn<RefreshToken>(
            name: "RefrehToken",
            schema: "Identity",
            table: "AspNetUsers",
            type: "jsonb",
            nullable: true);

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"),
            column: "RefrehToken",
            value: null);

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9"),
            column: "RefrehToken",
            value: null);

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9"),
            column: "RefrehToken",
            value: null);

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9"),
            column: "RefrehToken",
            value: null);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "RefrehToken",
            schema: "Identity",
            table: "AspNetUsers");

        migrationBuilder.AddColumn<string>(
            name: "RefreshToken",
            schema: "Identity",
            table: "AspNetUsers",
            type: "text",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "RefreshTokenEndDate",
            schema: "Identity",
            table: "AspNetUsers",
            type: "timestamp with time zone",
            nullable: true);

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"),
            columns: new[] { "RefreshToken", "RefreshTokenEndDate" },
            values: new object[] { null, null });

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9"),
            columns: new[] { "RefreshToken", "RefreshTokenEndDate" },
            values: new object[] { null, null });

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9"),
            columns: new[] { "RefreshToken", "RefreshTokenEndDate" },
            values: new object[] { null, null });

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9"),
            columns: new[] { "RefreshToken", "RefreshTokenEndDate" },
            values: new object[] { null, null });
    }
}
