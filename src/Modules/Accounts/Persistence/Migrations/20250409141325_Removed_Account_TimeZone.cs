﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Accounts.Persistence.Migrations;

/// <inheritdoc />
public partial class Removed_Account_TimeZone : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "TimeZone",
			schema: "Accounts",
			table: "Accounts");
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<string>(
			name: "TimeZone",
			schema: "Accounts",
			table: "Accounts",
			type: "text",
			nullable: false,
			defaultValue: "");

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("0fb3212f-7d51-4586-8fc2-0f333ec9fbc1"),
			column: "TimeZone",
			value: "Europe/Sofia");

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("2da61b05-1a27-4af9-9df2-be4f1f4e835f"),
			column: "TimeZone",
			value: "Europe/Sofia");

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("6d963818-23dc-4e9a-aaa8-b4c77252bc97"),
			column: "TimeZone",
			value: "Europe/Sofia");

		migrationBuilder.UpdateData(
			schema: "Accounts",
			table: "Accounts",
			keyColumn: "Id",
			keyValue: new Guid("e995039c-a535-4f20-8288-7aadcb71b252"),
			column: "TimeZone",
			value: "Europe/Sofia");
	}
}
