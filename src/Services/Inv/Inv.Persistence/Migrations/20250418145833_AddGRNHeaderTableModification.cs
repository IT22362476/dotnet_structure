using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGRNHeaderTableModification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                schema: "Inv",
                table: "GRNHeader",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                schema: "Inv",
                table: "GRNHeader",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNDetail_Item_ItemSerialID",
                schema: "Inv",
                table: "GRNDetail",
                column: "ItemSerialID",
                principalSchema: "Inv",
                principalTable: "Item",
                principalColumn: "ItemSerialID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRNDetail_Item_ItemSerialID",
                schema: "Inv",
                table: "GRNDetail");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                schema: "Inv",
                table: "GRNHeader");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                schema: "Inv",
                table: "GRNHeader");
        }
    }
}
