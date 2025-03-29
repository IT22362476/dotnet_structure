using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemDes",
                schema: "Inv",
                table: "Item");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovedDate",
                schema: "Inv",
                table: "Item",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                schema: "Inv",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "ApprovedDate",
                schema: "Inv",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "ItemDes",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
