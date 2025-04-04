using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Identity.Persistence.Migrations;

/// <inheritdoc />
public partial class Refined_Seeding : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"),
            column: "PasswordHash",
            value: "AQAAAAIAAYagAAAAEEUe31maWfuZY6V8MQBzUWKerMKobDukREinVfML3Yl2z+Nr6IIQZKvX4WKqbTUw6w==");

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9"),
            columns: new[] { "NormalizedUserName", "PasswordHash", "UserName" },
            values: new object[] { "PDMATSALIEV20", "AQAAAAIAAYagAAAAEGjQ1Zes3r2XJgjoHQykiyr11OgUEDw+YDnOKeENyN7Kqi9RWKKRCtwd7ZtEyywdYA==", "PDMatsaliev20" });

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9"),
            column: "PasswordHash",
            value: "AQAAAAIAAYagAAAAEFqtQ33BvarNRyFcmV4z48fPBlIY8zd0de90qq3Cdm1Row+2WRmEjVJk1yPadBkrSA==");

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9"),
            column: "PasswordHash",
            value: "AQAAAAIAAYagAAAAEJFCGOTxNAgjhqU5lrA63WEtv924ujxXHt0x1R70qlS8dV9Pzz4II8GOgjVOaRzuDQ==");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("4337a774-2c5c-4c27-d28b-08dd11623eb9"),
            column: "PasswordHash",
            value: "AQAAAAIAAYagAAAAEIuVU3Ziopa1Dv4t79ImAnluJSpVuJpvQawEaF/11u9szawuOWYd5yErqFGevwRHwg==");

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("af840410-f3f2-4a3b-d28a-08dd11623eb9"),
            columns: new[] { "NormalizedUserName", "PasswordHash", "UserName" },
            values: new object[] { "PETARDMATSALIEV", "AQAAAAIAAYagAAAAEJNqqiC31XGVrNSSflDLpuNzs/PIzg8VXCyEOL2hqvWAYi8a37bn5CUxHdvVuszSsQ==", "PetarDMatsaliev" });

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("cb7749fb-3fff-4902-d28c-08dd11623eb9"),
            column: "PasswordHash",
            value: "AQAAAAIAAYagAAAAEI/R+FhQaDs57q+Z94HwbWVhv8PXnUlhXb71NicOb2CQPwTgdN9C1bRsRAIsfijjsA==");

        migrationBuilder.UpdateData(
            schema: "Identity",
            table: "AspNetUsers",
            keyColumn: "Id",
            keyValue: new Guid("e38c495f-b1f3-4226-d289-08dd11623eb9"),
            column: "PasswordHash",
            value: "AQAAAAIAAYagAAAAEMxKo17QTeytzknDR27c10aVDBF1wGzycD+CSTbVliUg0h8g6f4U2AAQTh9YAoPXYw==");
    }
}
