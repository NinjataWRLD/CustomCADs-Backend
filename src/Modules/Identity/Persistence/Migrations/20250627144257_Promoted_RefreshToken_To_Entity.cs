using CustomCADs.Identity.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable


namespace CustomCADs.Identity.Persistence.Migrations;

/// <inheritdoc />
public partial class Promoted_RefreshToken_To_Entity : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "RefrehToken",
			schema: "Identity",
			table: "AspNetUsers");

		migrationBuilder.CreateTable(
			name: "AppRefreshToken",
			schema: "Identity",
			columns: table => new
			{
				Id = table.Column<Guid>(type: "uuid", nullable: false),
				IssuedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
				ExpiresAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
				Value = table.Column<string>(type: "text", nullable: false),
				UserId = table.Column<Guid>(type: "uuid", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_AppRefreshToken", x => x.Id);
				table.ForeignKey(
					name: "FK_AppRefreshToken_AspNetUsers_UserId",
					column: x => x.UserId,
					principalSchema: "Identity",
					principalTable: "AspNetUsers",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});

		migrationBuilder.CreateIndex(
			name: "IX_AppRefreshToken_UserId",
			schema: "Identity",
			table: "AppRefreshToken",
			column: "UserId");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "AppRefreshToken",
			schema: "Identity");

		migrationBuilder.AddColumn<RefreshToken>(
			name: "RefrehToken",
			schema: "Identity",
			table: "AspNetUsers",
			type: "jsonb",
			nullable: true);

		migrationBuilder.UpdateData(
			schema: "Identity",
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"),
			column: "RefrehToken",
			value: null);

		migrationBuilder.UpdateData(
			schema: "Identity",
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9"),
			column: "RefrehToken",
			value: null);

		migrationBuilder.UpdateData(
			schema: "Identity",
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9"),
			column: "RefrehToken",
			value: null);

		migrationBuilder.UpdateData(
			schema: "Identity",
			table: "AspNetUsers",
			keyColumn: "Id",
			keyValue: new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9"),
			column: "RefrehToken",
			value: null);
	}
}
