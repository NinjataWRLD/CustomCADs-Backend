using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomCADs.Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Added_AppUser_AccountId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                schema: "Auth",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountId",
                schema: "Auth",
                table: "AspNetUsers");
        }
    }
}
