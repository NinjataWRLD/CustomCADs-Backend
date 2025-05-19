using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Customs.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial_Migration : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			name: "Customs");

		migrationBuilder.CreateTable(
			name: "Customs",
			schema: "Customs",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uuid", nullable: false),
				Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
				Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
				ForDelivery = table.Column<bool>(type: "boolean", nullable: false),
				CustomStatus = table.Column<string>(type: "text", nullable: false),
				OrderedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
				BuyerId = table.Column<Guid>(type: "uuid", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_Customs", x => x.Id);
			});

		migrationBuilder.CreateTable(
			name: "AcceptedCustoms",
			schema: "Customs",
			columns: table => new
			{
				CustomId = table.Column<Guid>(type: "uuid", nullable: false),
				AcceptedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
				DesignerId = table.Column<Guid>(type: "uuid", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AcceptedCustoms", x => x.CustomId);
				table.ForeignKey(
					name: "FK_AcceptedCustoms_Customs_CustomId",
					column: x => x.CustomId,
					principalSchema: "Customs",
					principalTable: "Customs",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "CompletedCustoms",
			schema: "Customs",
			columns: table => new
			{
				CustomId = table.Column<Guid>(type: "uuid", nullable: false),
				CustomizationId = table.Column<Guid>(type: "uuid", nullable: true),
				ShipmentId = table.Column<Guid>(type: "uuid", nullable: true)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_CompletedCustoms", x => x.CustomId);
				table.ForeignKey(
					name: "FK_CompletedCustoms_Customs_CustomId",
					column: x => x.CustomId,
					principalSchema: "Customs",
					principalTable: "Customs",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateTable(
			name: "FinishedCustoms",
			schema: "Customs",
			columns: table => new
			{
				CustomId = table.Column<Guid>(type: "uuid", nullable: false),
				Price = table.Column<decimal>(type: "numeric(19,2)", precision: 19, scale: 2, nullable: false),
				FinishedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
				CadId = table.Column<Guid>(type: "uuid", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_FinishedCustoms", x => x.CustomId);
				table.ForeignKey(
					name: "FK_FinishedCustoms_Customs_CustomId",
					column: x => x.CustomId,
					principalSchema: "Customs",
					principalTable: "Customs",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "AcceptedCustoms",
			schema: "Customs");

		migrationBuilder.DropTable(
			name: "CompletedCustoms",
			schema: "Customs");

		migrationBuilder.DropTable(
			name: "FinishedCustoms",
			schema: "Customs");

		migrationBuilder.DropTable(
			name: "Customs",
			schema: "Customs");
	}
}
