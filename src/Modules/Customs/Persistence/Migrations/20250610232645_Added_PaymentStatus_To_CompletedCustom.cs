using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Customs.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_PaymentStatus_To_CompletedCustom : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<string>(
			name: "PaymentStatus",
			schema: "Customs",
			table: "CompletedCustoms",
			type: "text",
			nullable: false,
			defaultValue: "");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "PaymentStatus",
			schema: "Customs",
			table: "CompletedCustoms");
	}
}
