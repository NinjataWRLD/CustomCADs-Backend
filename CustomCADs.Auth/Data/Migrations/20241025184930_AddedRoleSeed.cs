using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Auth.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedRoleSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Auth",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("762ddec2-25c9-4183-9891-72a19d84a839"), "51da1b9f-803c-4bd3-9a00-da7ac259ce32", "Client", "CLIENT" },
                    { new Guid("e1101e2c-32cc-456f-9c82-4f1d1a65d141"), "a1a170e0-ee84-4afe-afd9-1df57009f291", "Contributor", "CONTRIBUTOR" },
                    { new Guid("f3ad41d3-ee90-4988-9195-8b2a8f4f2733"), "1a8ba0a7-4853-42da-980d-3107784e7ab1", "Designer", "DESIGNER" },
                    { new Guid("fad1b19d-5333-4633-bd84-d67c64649f65"), "42174679-32f1-48b0-9524-0f00791ec760", "Administrator", "ADMINISTRATOR" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("762ddec2-25c9-4183-9891-72a19d84a839"));

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e1101e2c-32cc-456f-9c82-4f1d1a65d141"));

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("f3ad41d3-ee90-4988-9195-8b2a8f4f2733"));

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("fad1b19d-5333-4633-bd84-d67c64649f65"));
        }
    }
}
