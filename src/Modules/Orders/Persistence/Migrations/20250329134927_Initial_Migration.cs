using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Orders.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial_Migration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "Orders");

        migrationBuilder.CreateTable(
            name: "CompletedOrders",
            schema: "Orders",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                Price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
                Delivery = table.Column<bool>(type: "boolean", nullable: false),
                OrderedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                PurchasedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                DesignerId = table.Column<Guid>(type: "uuid", nullable: false),
                CadId = table.Column<Guid>(type: "uuid", nullable: false),
                ShipmentId = table.Column<Guid>(type: "uuid", nullable: true),
                CustomizationId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_CompletedOrders", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "OngoingOrders",
            schema: "Orders",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                Price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: true),
                Delivery = table.Column<bool>(type: "boolean", nullable: false),
                OrderedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                OrderStatus = table.Column<string>(type: "text", nullable: false),
                BuyerId = table.Column<Guid>(type: "uuid", nullable: false),
                DesignerId = table.Column<Guid>(type: "uuid", nullable: true),
                CadId = table.Column<Guid>(type: "uuid", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OngoingOrders", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "CompletedOrders",
            schema: "Orders");

        migrationBuilder.DropTable(
            name: "OngoingOrders",
            schema: "Orders");
    }
}
