using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Idempotency.Persistence.Migrations;

/// <inheritdoc />
public partial class Made_ResponseBody_And_StatusCode_Nullable : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<int>(
			name: "StatusCode",
			schema: "Idempotency",
			table: "IdempotencyKeys",
			type: "integer",
			nullable: true,
			oldClrType: typeof(int),
			oldType: "integer");

		migrationBuilder.AlterColumn<string>(
			name: "ResponseBody",
			schema: "Idempotency",
			table: "IdempotencyKeys",
			type: "text",
			nullable: true,
			oldClrType: typeof(string),
			oldType: "text");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<int>(
			name: "StatusCode",
			schema: "Idempotency",
			table: "IdempotencyKeys",
			type: "integer",
			nullable: false,
			defaultValue: 0,
			oldClrType: typeof(int),
			oldType: "integer",
			oldNullable: true);

		migrationBuilder.AlterColumn<string>(
			name: "ResponseBody",
			schema: "Idempotency",
			table: "IdempotencyKeys",
			type: "text",
			nullable: false,
			defaultValue: "",
			oldClrType: typeof(string),
			oldType: "text",
			oldNullable: true);
	}
}
