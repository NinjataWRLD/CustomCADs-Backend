using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace CustomCADs.Idempotency.Persistence.Migrations;

/// <inheritdoc />
public partial class Initial_Migration : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.EnsureSchema(
			name: "Idempotency");

		migrationBuilder.CreateTable(
			name: "IdempotencyKeys",
			schema: "Idempotency",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uuid", nullable: false),
				RequestHash = table.Column<string>(type: "text", nullable: false),
				ResponseBody = table.Column<string>(type: "text", nullable: false),
				StatusCode = table.Column<int>(type: "integer", nullable: false),
				CreatedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_IdempotencyKeys", x => new { x.Id, x.RequestHash });
			});

		migrationBuilder.CreateIndex(
			name: "IX_IdempotencyKeys_CreatedAt",
			schema: "Idempotency",
			table: "IdempotencyKeys",
			column: "CreatedAt");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "IdempotencyKeys",
			schema: "Idempotency");
	}
}
