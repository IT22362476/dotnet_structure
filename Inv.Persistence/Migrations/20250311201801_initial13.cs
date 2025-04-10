using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_GRNLineItem_Item_ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem");

            migrationBuilder.DropColumn(
                name: "LineTotal",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropColumn(
                name: "Quantity",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.RenameColumn(
                name: "UnitCost",
                schema: "Inv",
                table: "GRNLineItem",
                newName: "ReceivedQty");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                schema: "Inv",
                table: "PurchaseOrderItem",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "POItemPOSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "POItemSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "POSerialID",
                schema: "Inv",
                table: "GRN",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_GRNLineItem_POItemPOSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "POItemPOSerialID");

            migrationBuilder.AddForeignKey(
                name: "FK_GRNLineItem_Item_ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "ItemSerialID",
                principalSchema: "Inv",
                principalTable: "Item",
                principalColumn: "ItemSerialID");

            migrationBuilder.AddForeignKey(
                name: "FK_GRNLineItem_PurchaseOrderItem_POItemPOSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "POItemPOSerialID",
                principalSchema: "Inv",
                principalTable: "PurchaseOrderItem",
                principalColumn: "POSerialID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem",
                column: "PurchaseOrderPOSerialID",
                principalSchema: "Inv",
                principalTable: "PurchaseOrder",
                principalColumn: "POSerialID");*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropForeignKey(
                name: "FK_GRNLineItem_Item_ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNLineItem_PurchaseOrderItem_POItemPOSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem");

            migrationBuilder.DropIndex(
                name: "IX_GRNLineItem_POItemPOSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropColumn(
                name: "POItemPOSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropColumn(
                name: "POItemSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropColumn(
                name: "POSerialID",
                schema: "Inv",
                table: "GRN");

            migrationBuilder.RenameColumn(
                name: "ReceivedQty",
                schema: "Inv",
                table: "GRNLineItem",
                newName: "UnitCost");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                schema: "Inv",
                table: "PurchaseOrderItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LineTotal",
                schema: "Inv",
                table: "GRNLineItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Quantity",
                schema: "Inv",
                table: "GRNLineItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNLineItem_Item_ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "ItemSerialID",
                principalSchema: "Inv",
                principalTable: "Item",
                principalColumn: "ItemSerialID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem",
                column: "PurchaseOrderPOSerialID",
                principalSchema: "Inv",
                principalTable: "PurchaseOrder",
                principalColumn: "POSerialID",
                onDelete: ReferentialAction.Cascade);*/
        }
    }
}
