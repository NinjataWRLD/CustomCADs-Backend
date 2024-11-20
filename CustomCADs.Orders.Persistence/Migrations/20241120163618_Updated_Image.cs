using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Orders.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                schema: "Orders",
                table: "Orders",
                newName: "ImageKey");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageKey",
                schema: "Orders",
                table: "Orders",
                newName: "ImagePath");
        }
    }
}
