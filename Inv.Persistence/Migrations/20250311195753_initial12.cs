using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "InvoiceItemID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "POID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "POItemID",
                schema: "dbo");

            migrationBuilder.AddColumn<int>(
                name: "Code",
                schema: "Inv",
                table: "ItemType",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                schema: "Inv",
                table: "Invoice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InvoiceItem",
                schema: "Inv",
                columns: table => new
                {
                    InvoiceSerialID = table.Column<int>(type: "int", nullable: false),
                    InvoiceItemSerialID = table.Column<int>(type: "int", nullable: false),
                    InvoiceItemID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.InvoiceItemID"),
                    GRNLineItemSerialID = table.Column<int>(type: "int", nullable: false),
                    ItemSerialID = table.Column<int>(type: "int", nullable: false),
                    BilledQty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BilledAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceItem", x => x.InvoiceSerialID);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_GRNLineItem_GRNLineItemSerialID",
                        column: x => x.GRNLineItemSerialID,
                        principalSchema: "Inv",
                        principalTable: "GRNLineItem",
                        principalColumn: "GRNLineItemSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InvoiceItem_Invoice_InvoiceSerialID",
                        column: x => x.InvoiceSerialID,
                        principalSchema: "Inv",
                        principalTable: "Invoice",
                        principalColumn: "InvoiceSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrder",
                schema: "Inv",
                columns: table => new
                {
                    POSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.POID"),
                    SupplierSerialID = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrder", x => x.POSerialID);
                  });

            migrationBuilder.CreateTable(
                name: "PurchaseOrderItem",
                schema: "Inv",
                columns: table => new
                {
                    POSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    POItemSerialID = table.Column<int>(type: "int", nullable: false),
                    POItemID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.POItemID"),
                    ItemSerialID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseOrderPOSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrderItem", x => x.POSerialID);
                    table.ForeignKey(
                        name: "FK_PurchaseOrderItem_PurchaseOrder_PurchaseOrderPOSerialID",
                        column: x => x.PurchaseOrderPOSerialID,
                        principalSchema: "Inv",
                        principalTable: "PurchaseOrder",
                        principalColumn: "POSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GRN_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN",
                column: "PurchaseOrderPOSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_GRNLineItemSerialID",
                schema: "Inv",
                table: "InvoiceItem",
                column: "GRNLineItemSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_InvoiceItemSerialID",
                schema: "Inv",
                table: "InvoiceItem",
                column: "InvoiceItemSerialID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceItem_IsDeleted",
                schema: "Inv",
                table: "InvoiceItem",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_IsDeleted",
                schema: "Inv",
                table: "PurchaseOrder",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_POID",
                schema: "Inv",
                table: "PurchaseOrder",
                column: "POID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrder_SupplierSerialId",
                schema: "Inv",
                table: "PurchaseOrder",
                column: "SupplierSerialId");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_IsDeleted",
                schema: "Inv",
                table: "PurchaseOrderItem",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_POItemID",
                schema: "Inv",
                table: "PurchaseOrderItem",
                column: "POItemID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrderItem_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "PurchaseOrderItem",
                column: "PurchaseOrderPOSerialID");

            migrationBuilder.AddForeignKey(
                name: "FK_GRN_PurchaseOrder_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN",
                column: "PurchaseOrderPOSerialID",
                principalSchema: "Inv",
                principalTable: "PurchaseOrder",
                principalColumn: "POSerialID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRN_PurchaseOrder_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN");

            migrationBuilder.DropTable(
                name: "InvoiceItem",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "PurchaseOrderItem",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "PurchaseOrder",
                schema: "Inv");

            migrationBuilder.DropIndex(
                name: "IX_GRN_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN");

            migrationBuilder.DropColumn(
                name: "Code",
                schema: "Inv",
                table: "ItemType");

            migrationBuilder.DropColumn(
                name: "Status",
                schema: "Inv",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN");

            migrationBuilder.DropSequence(
                name: "InvoiceItemID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "POID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "POItemID",
                schema: "dbo");
        }
    }
}
