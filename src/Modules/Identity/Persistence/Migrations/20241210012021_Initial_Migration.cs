using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Identity.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial_Migration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Identity");

        migrationBuilder.CreateTable(
            name: "AspNetRoles",
            schema: "Identity",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUsers",
            schema: "Identity",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                AccountId = table.Column<Guid>(type: "uuid", nullable: false),
                RefreshToken = table.Column<string>(type: "text", nullable: true),
                RefreshTokenEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                PasswordHash = table.Column<string>(type: "text", nullable: true),
                SecurityStamp = table.Column<string>(type: "text", nullable: true),
                ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                PhoneNumber = table.Column<string>(type: "text", nullable: true),
                PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUsers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "AspNetRoleClaims",
            schema: "Identity",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                ClaimType = table.Column<string>(type: "text", nullable: true),
                ClaimValue = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "Identity",
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserClaims",
            schema: "Identity",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                ClaimType = table.Column<string>(type: "text", nullable: true),
                ClaimValue = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                table.ForeignKey(
                    name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalSchema: "Identity",
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserLogins",
            schema: "Identity",
            columns: table => new
            {
                LoginProvider = table.Column<string>(type: "text", nullable: false),
                ProviderKey = table.Column<string>(type: "text", nullable: false),
                ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                UserId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                table.ForeignKey(
                    name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalSchema: "Identity",
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserRoles",
            schema: "Identity",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                RoleId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "Identity",
                    principalTable: "AspNetRoles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalSchema: "Identity",
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "AspNetUserTokens",
            schema: "Identity",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uuid", nullable: false),
                LoginProvider = table.Column<string>(type: "text", nullable: false),
                Name = table.Column<string>(type: "text", nullable: false),
                Value = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                table.ForeignKey(
                    name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                    column: x => x.UserId,
                    principalSchema: "Identity",
                    principalTable: "AspNetUsers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            schema: "Identity",
            table: "AspNetRoles",
            columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
            values: new object[,]
            {
                { new Guid("762ddec2-25c9-4183-9891-72a19d84a839"), "51da1b9f-803c-4bd3-9a00-da7ac259ce32", "Client", "CLIENT" },
                { new Guid("e1101e2c-32cc-456f-9c82-4f1d1a65d141"), "a1a170e0-ee84-4afe-afd9-1df57009f291", "Contributor", "CONTRIBUTOR" },
                { new Guid("f3ad41d3-ee90-4988-9195-8b2a8f4f2733"), "1a8ba0a7-4853-42da-980d-3107784e7ab1", "Designer", "DESIGNER" },
                { new Guid("fad1b19d-5333-4633-bd84-d67c64649f65"), "42174679-32f1-48b0-9524-0f00791ec760", "Administrator", "ADMINISTRATOR" }
            });

        migrationBuilder.InsertData(
            schema: "Identity",
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
            schema: "Identity",
            table: "AspNetUserRoles",
            columns: new[] { "RoleId", "UserId" },
            values: new object[,]
            {
                { new Guid("f3ad41d3-ee90-4988-9195-8b2a8f4f2733"), new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9") },
                { new Guid("e1101e2c-32cc-456f-9c82-4f1d1a65d141"), new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9") },
                { new Guid("fad1b19d-5333-4633-bd84-d67c64649f65"), new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9") },
                { new Guid("762ddec2-25c9-4183-9891-72a19d84a839"), new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9") }
            });

        migrationBuilder.CreateIndex(
            name: "IX_AspNetRoleClaims_RoleId",
            schema: "Identity",
            table: "AspNetRoleClaims",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "RoleNameIndex",
            schema: "Identity",
            table: "AspNetRoles",
            column: "NormalizedName",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserClaims_UserId",
            schema: "Identity",
            table: "AspNetUserClaims",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserLogins_UserId",
            schema: "Identity",
            table: "AspNetUserLogins",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_AspNetUserRoles_RoleId",
            schema: "Identity",
            table: "AspNetUserRoles",
            column: "RoleId");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            schema: "Identity",
            table: "AspNetUsers",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            schema: "Identity",
            table: "AspNetUsers",
            column: "NormalizedUserName",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "AspNetRoleClaims",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "AspNetUserClaims",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "AspNetUserLogins",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "AspNetUserRoles",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "AspNetUserTokens",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "AspNetRoles",
            schema: "Identity");

        migrationBuilder.DropTable(
            name: "AspNetUsers",
            schema: "Identity");
    }
}
