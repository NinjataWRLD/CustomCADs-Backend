using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Carts.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_AddedAt_To_CartItems : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<DateTimeOffset>(
			name: "AddedAt",
			schema: "Carts",
			table: "PurchasedCartItems",
			type: "timestamp with time zone",
			nullable: false,
			defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

		migrationBuilder.AddColumn<DateTimeOffset>(
			name: "AddedAt",
			schema: "Carts",
			table: "ActiveCartItems",
			type: "timestamp with time zone",
			nullable: false,
			defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "AddedAt",
			schema: "Carts",
			table: "PurchasedCartItems");

		migrationBuilder.DropColumn(
			name: "AddedAt",
			schema: "Carts",
			table: "ActiveCartItems");
	}
}
