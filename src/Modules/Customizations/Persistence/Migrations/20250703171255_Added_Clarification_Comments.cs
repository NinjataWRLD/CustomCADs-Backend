using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Customizations.Persistence.Migrations;

/// <inheritdoc />
public partial class Added_Clarification_Comments : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<decimal>(
			name: "Density",
			schema: "Customizations",
			table: "Materials",
			type: "numeric(18,2)",
			precision: 18,
			scale: 2,
			nullable: false,
			comment: "Measured in g/cm³",
			oldClrType: typeof(decimal),
			oldType: "numeric(18,2)",
			oldPrecision: 18,
			oldScale: 2);

		migrationBuilder.AlterColumn<decimal>(
			name: "Cost",
			schema: "Customizations",
			table: "Materials",
			type: "numeric(18,2)",
			precision: 18,
			scale: 2,
			nullable: false,
			comment: "Measured in EUR/kg",
			oldClrType: typeof(decimal),
			oldType: "numeric(18,2)",
			oldPrecision: 18,
			oldScale: 2);

		migrationBuilder.AlterColumn<decimal>(
			name: "Volume",
			schema: "Customizations",
			table: "Customizations",
			type: "numeric(18,2)",
			precision: 18,
			scale: 2,
			nullable: false,
			comment: "Measured in m³",
			oldClrType: typeof(decimal),
			oldType: "numeric(18,2)",
			oldPrecision: 18,
			oldScale: 2);

		migrationBuilder.AlterColumn<decimal>(
			name: "Scale",
			schema: "Customizations",
			table: "Customizations",
			type: "numeric(4,2)",
			precision: 4,
			scale: 2,
			nullable: false,
			comment: "Floating number representing a percentage",
			oldClrType: typeof(decimal),
			oldType: "numeric(4,2)",
			oldPrecision: 4,
			oldScale: 2);

		migrationBuilder.AlterColumn<decimal>(
			name: "Infill",
			schema: "Customizations",
			table: "Customizations",
			type: "numeric(4,2)",
			precision: 4,
			scale: 2,
			nullable: false,
			comment: "Floating number representing a percentage",
			oldClrType: typeof(decimal),
			oldType: "numeric(4,2)",
			oldPrecision: 4,
			oldScale: 2);

		migrationBuilder.AlterColumn<string>(
			name: "Color",
			schema: "Customizations",
			table: "Customizations",
			type: "character varying(7)",
			maxLength: 7,
			nullable: false,
			comment: "Hexadecimal value of color",
			oldClrType: typeof(string),
			oldType: "character varying(7)",
			oldMaxLength: 7);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AlterColumn<decimal>(
			name: "Density",
			schema: "Customizations",
			table: "Materials",
			type: "numeric(18,2)",
			precision: 18,
			scale: 2,
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "numeric(18,2)",
			oldPrecision: 18,
			oldScale: 2,
			oldComment: "Measured in g/cm³");

		migrationBuilder.AlterColumn<decimal>(
			name: "Cost",
			schema: "Customizations",
			table: "Materials",
			type: "numeric(18,2)",
			precision: 18,
			scale: 2,
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "numeric(18,2)",
			oldPrecision: 18,
			oldScale: 2,
			oldComment: "Measured in EUR/kg");

		migrationBuilder.AlterColumn<decimal>(
			name: "Volume",
			schema: "Customizations",
			table: "Customizations",
			type: "numeric(18,2)",
			precision: 18,
			scale: 2,
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "numeric(18,2)",
			oldPrecision: 18,
			oldScale: 2,
			oldComment: "Measured in m³");

		migrationBuilder.AlterColumn<decimal>(
			name: "Scale",
			schema: "Customizations",
			table: "Customizations",
			type: "numeric(4,2)",
			precision: 4,
			scale: 2,
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "numeric(4,2)",
			oldPrecision: 4,
			oldScale: 2,
			oldComment: "Floating number representing a percentage");

		migrationBuilder.AlterColumn<decimal>(
			name: "Infill",
			schema: "Customizations",
			table: "Customizations",
			type: "numeric(4,2)",
			precision: 4,
			scale: 2,
			nullable: false,
			oldClrType: typeof(decimal),
			oldType: "numeric(4,2)",
			oldPrecision: 4,
			oldScale: 2,
			oldComment: "Floating number representing a percentage");

		migrationBuilder.AlterColumn<string>(
			name: "Color",
			schema: "Customizations",
			table: "Customizations",
			type: "character varying(7)",
			maxLength: 7,
			nullable: false,
			oldClrType: typeof(string),
			oldType: "character varying(7)",
			oldMaxLength: 7,
			oldComment: "Hexadecimal value of color");
	}
}
