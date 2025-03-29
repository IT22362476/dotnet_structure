using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Invoice_GRNSerialID",
                schema: "Inv",
                table: "Invoice",
                column: "GRNSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_SupplierSerialID",
                schema: "Inv",
                table: "Invoice",
                column: "SupplierSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_GRNLineItem_GRNSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "GRNSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_GRNLineItem_ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "ItemSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_GRN_SupplierSerialID",
                schema: "Inv",
                table: "GRN",
                column: "SupplierSerialID");

            migrationBuilder.AddForeignKey(
                name: "FK_GRN_Supplier_SupplierSerialID",
                schema: "Inv",
                table: "GRN",
                column: "SupplierSerialID",
                principalSchema: "Inv",
                principalTable: "Supplier",
                principalColumn: "SupplierSerialId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNLineItem_GRN_GRNSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "GRNSerialID",
                principalSchema: "Inv",
                principalTable: "GRN",
                principalColumn: "GRNSerialID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GRNLineItem_Item_ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem",
                column: "ItemSerialID",
                principalSchema: "Inv",
                principalTable: "Item",
                principalColumn: "ItemSerialID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_GRN_GRNSerialID",
                schema: "Inv",
                table: "Invoice",
                column: "GRNSerialID",
                principalSchema: "Inv",
                principalTable: "GRN",
                principalColumn: "GRNSerialID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Supplier_SupplierSerialID",
                schema: "Inv",
                table: "Invoice",
                column: "SupplierSerialID",
                principalSchema: "Inv",
                principalTable: "Supplier",
                principalColumn: "SupplierSerialId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRN_Supplier_SupplierSerialID",
                schema: "Inv",
                table: "GRN");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNLineItem_GRN_GRNSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropForeignKey(
                name: "FK_GRNLineItem_Item_ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_GRN_GRNSerialID",
                schema: "Inv",
                table: "Invoice");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Supplier_SupplierSerialID",
                schema: "Inv",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_GRNSerialID",
                schema: "Inv",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_SupplierSerialID",
                schema: "Inv",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_GRNLineItem_GRNSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropIndex(
                name: "IX_GRNLineItem_ItemSerialID",
                schema: "Inv",
                table: "GRNLineItem");

            migrationBuilder.DropIndex(
                name: "IX_GRN_SupplierSerialID",
                schema: "Inv",
                table: "GRN");
        }
    }
}
