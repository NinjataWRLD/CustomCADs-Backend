using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Cads.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Cad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientId",
                schema: "Cads",
                table: "Cads");

            migrationBuilder.RenameColumn(
                name: "Path",
                schema: "Cads",
                table: "Cads",
                newName: "Key");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Key",
                schema: "Cads",
                table: "Cads",
                newName: "Path");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientId",
                schema: "Cads",
                table: "Cads",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
