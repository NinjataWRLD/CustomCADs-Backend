using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Cads.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Cads");

            migrationBuilder.CreateTable(
                name: "Cads",
                schema: "Cads",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CamX = table.Column<int>(type: "int", nullable: false),
                    CamY = table.Column<int>(type: "int", nullable: false),
                    CamZ = table.Column<int>(type: "int", nullable: false),
                    PanX = table.Column<int>(type: "int", nullable: false),
                    PanY = table.Column<int>(type: "int", nullable: false),
                    PanZ = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cads", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cads",
                schema: "Cads");
        }
    }
}
