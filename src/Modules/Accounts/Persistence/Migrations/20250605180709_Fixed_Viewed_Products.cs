using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Accounts.Persistence.Migrations;

/// <inheritdoc />
public partial class Fixed_Viewed_Products : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "ViewedProductIds",
			schema: "Accounts",
			table: "Accounts");

		migrationBuilder.CreateTable(
			name: "ViewedProducts",
			schema: "Accounts",
			columns: table => new
			{
				AccountId = table.Column<Guid>(type: "uuid", nullable: false),
				ProductId = table.Column<Guid>(type: "uuid", nullable: false)
			},
			constraints: table =>
			{
				table.PrimaryKey("PK_ViewedProducts", x => new { x.AccountId, x.ProductId });
				table.ForeignKey(
					name: "FK_ViewedProducts_Accounts_AccountId",
					column: x => x.AccountId,
					principalSchema: "Accounts",
					principalTable: "Accounts",
					principalColumn: "Id",
					onDelete: ReferentialAction.Cascade);
			});
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropTable(
			name: "ViewedProducts",
			schema: "Accounts");

		migrationBuilder.AddColumn<Guid[]>(
			name: "ViewedProductIds",
			schema: "Accounts",
			table: "Accounts",
			type: "uuid[]",
			nullable: false,
			defaultValue: new Guid[0]);

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"),
			column: "ViewedProductIds",
			value: new Guid[0]);

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("2da61b05-1a27-4af9-9df2-be4f1f4e835f"),
			column: "ViewedProductIds",
			value: new Guid[0]);

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"),
			column: "ViewedProductIds",
			value: new Guid[0]);

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("e995039c-a535-4f20-8288-7aadcb71b252"),
			column: "ViewedProductIds",
			value: new Guid[0]);
	}
}
