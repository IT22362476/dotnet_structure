using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.CreateSequence<int>(
                name: "GRNID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "GRNLineItemID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "InvoiceID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "SupplierId",
                schema: "dbo");*/

   /*         migrationBuilder.CreateSequence<int>(
                name: "TheNumberID",
                schema: "dbo");*/

            /*migrationBuilder.AlterColumn<int>(
                name: "LastNumber",
                schema: "Inv",
                table: "TheNumbers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);*/

      /*      migrationBuilder.AddColumn<bool>(
                name: "IsVoidBinLocation",
                schema: "Inv",
                table: "BinLocation",
                type: "bit",
                nullable: false,
                defaultValue: false);*/

         /*   migrationBuilder.AddColumn<int>(
                name: "ItemCondition",
                schema: "Inv",
                table: "BinLocation",
                type: "int",
                nullable: false,
                defaultValue: 0);*/

            /*migrationBuilder.CreateTable(
                name: "GRN",
                schema: "Inv",
                columns: table => new
                {
                    GRNSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.GRNID"),
                    SupplierSerialID = table.Column<int>(type: "int", nullable: false),
                    ReceivedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GRNNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRN", x => x.GRNSerialID);
                });

            migrationBuilder.CreateTable(
                name: "GRNLineItem",
                schema: "Inv",
                columns: table => new
                {
                    GRNLineItemSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GRNLineItemID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.GRNLineItemID"),
                    ItemSerialID = table.Column<int>(type: "int", nullable: false),
                    GRNSerialID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnitCost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LineTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GRNLineItem", x => x.GRNLineItemSerialID);
                });

            migrationBuilder.CreateTable(
                name: "Invoice",
                schema: "Inv",
                columns: table => new
                {
                    InvoiceSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.InvoiceID"),
                    SupplierSerialID = table.Column<int>(type: "int", nullable: false),
                    GRNSerialID = table.Column<int>(type: "int", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastNumber = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceSerialID);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                schema: "Inv",
                columns: table => new
                {
                    SupplierSerialId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.SupplierId"),
                    SupplierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ContactPerson = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierSerialId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GRN_GRNID",
                schema: "Inv",
                table: "GRN",
                column: "GRNID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GRN_IsDeleted",
                schema: "Inv",
                table: "GRN",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_GRNLineItem_GRNLineItemID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "GRNLineItemID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GRNLineItem_IsDeleted",
                schema: "Inv",
                table: "GRNLineItem",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_InvoiceID",
                schema: "Inv",
                table: "Invoice",
                column: "InvoiceID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_IsDeleted",
                schema: "Inv",
                table: "Invoice",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_IsDeleted",
                schema: "Inv",
                table: "Supplier",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Supplier_SupplierId",
                schema: "Inv",
                table: "Supplier",
                column: "SupplierId",
                unique: true);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropTable(
                name: "GRN",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "GRNLineItem",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "Invoice",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "Supplier",
                schema: "Inv");*/

         /*   migrationBuilder.DropColumn(
                name: "IsVoidBinLocation",
                schema: "Inv",
                table: "BinLocation");*/

       /*     migrationBuilder.DropColumn(
                name: "ItemCondition",
                schema: "Inv",
                table: "BinLocation");*/

            /*migrationBuilder.DropSequence(
                name: "GRNID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "GRNLineItemID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "InvoiceID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "SupplierId",
                schema: "dbo");*/

   /*         migrationBuilder.DropSequence(
                name: "TheNumberID",
                schema: "dbo");*/

            /*migrationBuilder.AlterColumn<int>(
                name: "LastNumber",
                schema: "Inv",
                table: "TheNumbers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");*/
        }
    }
}
