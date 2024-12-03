using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Identity.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seeded_Users : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Auth",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AccountId", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "RefreshToken", "RefreshTokenEndDate", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"), 0, new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"), "c5940d6f-d5c0-4f84-a262-da9b07525c3c", "boriskolev2006@gmail.com", true, true, null, "BORISKOLEV2006@GMAIL.COM", "ORACLE3000", "AQAAAAIAAYagAAAAEIuVU3Ziopa1Dv4t79ImAnluJSpVuJpvQawEaF/11u9szawuOWYd5yErqFGevwRHwg==", null, false, null, null, "FNNIT3NPOZKZK2E67WFLV5R3RGVBX7LV", false, "Oracle3000" },
                    { new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9"), 0, new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"), "c77927de-61e7-4d53-be8d-a5390fafc75c", "PDMatsaliev20@codingburgas.bg", true, true, null, "PDMATSALIEV20@CODINGBURGAS.BG", "PETARDMATSALIEV", "AQAAAAIAAYagAAAAEJNqqiC31XGVrNSSflDLpuNzs/PIzg8VXCyEOL2hqvWAYi8a37bn5CUxHdvVuszSsQ==", null, false, null, null, "NWGZ3JTQSDNS346DMU7RP4IT4BDLHIQC", false, "PetarDMatsaliev" },
                    { new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9"), 0, new Guid("e995039c-a535-4f20-8288-7aadcb71b252"), "5c94b43f-861c-4efa-a670-5627e49d354d", "ivanangelov414@gmail.com", true, true, null, "IVANANGELOV414@GMAIL.COM", "NINJATABG", "AQAAAAIAAYagAAAAEI/R+FhQaDs57q+Z94HwbWVhv8PXnUlhXb71NicOb2CQPwTgdN9C1bRsRAIsfijjsA==", null, false, null, null, "YIA26UZDSN2V2U5PVDEK4F3EJS3P5D3X", false, "NinjataBG" },
                    { new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9"), 0, new Guid("2da61b05-1a27-4af9-9df2-be4f1f4e835f"), "0c5bbfb2-d132-407b-9b1b-e1e640ccc14e", "ivanzlatinov006@gmail.com", true, true, null, "IVANZLATINOV006@GMAIL.COM", "FOR7A7A", "AQAAAAIAAYagAAAAEMxKo17QTeytzknDR27c10aVDBF1wGzycD+CSTbVliUg0h8g6f4U2AAQTh9YAoPXYw==", null, false, null, null, "3A6TFN6VVZNRZEG22J777XJTPQY7342B", false, "For7a7a" }
                });

            migrationBuilder.InsertData(
                schema: "Auth",
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { new Guid("f3ad41d3-ee90-4988-9195-8b2a8f4f2733"), new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9") },
                    { new Guid("e1101e2c-32cc-456f-9c82-4f1d1a65d141"), new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9") },
                    { new Guid("fad1b19d-5333-4633-bd84-d67c64649f65"), new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9") },
                    { new Guid("762ddec2-25c9-4183-9891-72a19d84a839"), new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("f3ad41d3-ee90-4988-9195-8b2a8f4f2733"), new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9") });

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("e1101e2c-32cc-456f-9c82-4f1d1a65d141"), new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9") });

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("fad1b19d-5333-4633-bd84-d67c64649f65"), new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9") });

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("762ddec2-25c9-4183-9891-72a19d84a839"), new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9") });

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"));

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9"));

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9"));

            migrationBuilder.DeleteData(
                schema: "Auth",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9"));
        }
    }
}
