﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.AddColumn<string>(
                name: "BinName",
                schema: "Inv",
                table: "BinLocation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropColumn(
                name: "BinName",
                schema: "Inv",
                table: "BinLocation");*/
        }
    }
}
