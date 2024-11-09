using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomCADs.Account.Persistence.Migrations;

/// <inheritdoc />
public partial class Reset_Migrations : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Account");

        migrationBuilder.CreateTable(
            name: "Roles",
            schema: "Account",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.Id);
                table.UniqueConstraint("AK_Roles_Name", x => x.Name);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            schema: "Account",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Username = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: false),
                Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: true),
                LastName = table.Column<string>(type: "nvarchar(62)", maxLength: 62, nullable: true),
                RoleName = table.Column<string>(type: "nvarchar(20)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.Id);
                table.ForeignKey(
                    name: "FK_Users_Roles_RoleName",
                    column: x => x.RoleName,
                    principalSchema: "Account",
                    principalTable: "Roles",
                    principalColumn: "Name",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            schema: "Account",
            table: "Roles",
            columns: new[] { "Id", "Description", "Name" },
            values: new object[,]
            {
                { 1, "Has access to Orders endpoints - can buy 3D Models from the Gallery and make and track Orders.", "Client" },
                { 2, "Has access to Cads endpoints - can apply to upload his 3D Models to the Gallery, set their prices and track their status", "Contributor" },
                { 3, "Has access to Cads and Designer endpoints - can upload his 3D Models straight to the Gallery, validate contributors' cads and finish clients' orders.", "Designer" },
                { 4, "Has full access to Users, Roles, Orders, Cads, Categories and all other endpoints - can do anyhting.", "Administrator" }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Users_RoleName",
            schema: "Account",
            table: "Users",
            column: "RoleName");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Users",
            schema: "Account");

        migrationBuilder.DropTable(
            name: "Roles",
            schema: "Account");
    }
}
