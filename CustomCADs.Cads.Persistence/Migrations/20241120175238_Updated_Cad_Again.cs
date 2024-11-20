using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Cads.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Cad_Again : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContentType",
                schema: "Cads",
                table: "Cads",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContentType",
                schema: "Cads",
                table: "Cads");
        }
    }
}
