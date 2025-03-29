using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Delivery.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial_Migration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Delivery");

        migrationBuilder.CreateTable(
            name: "Shipments",
            schema: "Delivery",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                ReferenceId = table.Column<string>(type: "text", nullable: false),
                RequestedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                City = table.Column<string>(type: "text", nullable: false),
                Country = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Shipments", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Shipments",
            schema: "Delivery");
    }
}
