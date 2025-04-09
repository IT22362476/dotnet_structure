using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddGRNTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
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
                name: "IX_PurchaseOrderItem_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem");

            migrationBuilder.DropIndex(
                name: "IX_GRNLineItem_ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropIndex(
                name: "IX_GRNLineItem_POItemPOSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem");

            migrationBuilder.DropColumn(
                name: "ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropColumn(
                name: "POItemPOSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.RenameColumn(
                name: "Code",
                schema: "Inv",
                table: "ItemType",
                newName: "ItemTypeCode");

            migrationBuilder.AlterColumn<int>(
                name: "POSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.CreateTable(
                name: "GRNHeader",
                schema: "Inv",
                columns: table => new
                {
                    GRNHeaderSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNID = table.Column<int>(type: "int", nullable: false),
                    CompSerialID = table.Column<int>(type: "int", nullable: false),
                    SupplierSerialID = table.Column<int>(type: "int", nullable: false),
                    SupplierInvoiceNumber = table.Column<int>(type: "int", nullable: false),
                    WHSerialID = table.Column<int>(type: "int", nullable: false),
                    StoreSerialID = table.Column<int>(type: "int", nullable: false),
                    Printed = table.Column<bool>(type: "bit", nullable: false),
                    PreparedBy = table.Column<int>(type: "int", nullable: false),
                    ApprovedBy = table.Column<int>(type: "int", nullable: true),
                    ApprovedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRNHeader", x => x.GRNHeaderSerialID);
                    table.ForeignKey(
                        name: "FK_GRNHeader_Store_StoreSerialID",
                        column: x => x.StoreSerialID,
                        principalSchema: "Inv",
                        principalTable: "Store",
                        principalColumn: "StoreSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GRNHeader_Warehouse_WHSerialID",
                        column: x => x.WHSerialID,
                        principalSchema: "Inv",
                        principalTable: "Warehouse",
                        principalColumn: "WHSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GRNDetail",
                schema: "Inv",
                columns: table => new
                {
                    GRNDetailSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNHeaderSerialID = table.Column<int>(type: "int", nullable: false),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    ItemSerialID = table.Column<int>(type: "int", nullable: false),
                    SystemPOSerialID = table.Column<int>(type: "int", nullable: false),
                    BatchNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WarrentyPeriod = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UOMSerialID = table.Column<int>(type: "int", nullable: false),
                    Condition = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FOCQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BatchBalQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AssetCount = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRNDetail", x => x.GRNDetailSerialID);
                    table.ForeignKey(
                        name: "FK_GRNDetail_GRNHeader_GRNHeaderSerialID",
                        column: x => x.GRNHeaderSerialID,
                        principalSchema: "Inv",
                        principalTable: "GRNHeader",
                        principalColumn: "GRNHeaderSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GRNDetail_UOM_UOMSerialID",
                        column: x => x.UOMSerialID,
                        principalSchema: "Inv",
                        principalTable: "UOM",
                        principalColumn: "UOMSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GRNLineItem_POItemSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "POItemSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_GRNDetail_GRNHeaderSerialID",
                schema: "Inv",
                table: "GRNDetail",
                column: "GRNHeaderSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_GRNDetail_IsDeleted",
                schema: "Inv",
                table: "GRNDetail",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_GRNDetail_UOMSerialID",
                schema: "Inv",
                table: "GRNDetail",
                column: "UOMSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_GRNHeader_IsDeleted",
                schema: "Inv",
                table: "GRNHeader",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_GRNHeader_StoreSerialID",
                schema: "Inv",
                table: "GRNHeader",
                column: "StoreSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_GRNHeader_WHSerialID",
                schema: "Inv",
                table: "GRNHeader",
                column: "WHSerialID");

            migrationBuilder.AddForeignKey(
                name: "FK_GRNLineItem_PurchaseOrderItem_POItemSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "POItemSerialID",
                principalSchema: "Inv",
                principalTable: "PurchaseOrderItem",
                principalColumn: "POSerialID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrderItem_PurchaseOrder_POSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem",
                column: "POSerialID",
                principalSchema: "Inv",
                principalTable: "PurchaseOrder",
                principalColumn: "POSerialID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRNLineItem_PurchaseOrderItem_POItemSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrderItem_PurchaseOrder_POSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem");

            migrationBuilder.DropTable(
                name: "GRNDetail",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "GRNHeader",
                schema: "Inv");

            migrationBuilder.DropIndex(
                name: "IX_GRNLineItem_POItemSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.RenameColumn(
                name: "ItemTypeCode",
                schema: "Inv",
                table: "ItemType",
                newName: "Code");

            migrationBuilder.AlterColumn<int>(
                name: "POSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "POItemPOSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem",
                column: "PurchaseOrderPOSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_GRNLineItem_ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "ItemSerialID");

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
                principalColumn: "POSerialID");
        }
    }
}
