using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRN_PurchaseOrder_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN");

 /*           migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Supplier_SupplierSerialId",
                schema: "Inv",
                table: "PurchaseOrder");*/

            migrationBuilder.DropIndex(
                name: "IX_GRN_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN");

/*            migrationBuilder.DropColumn(
                name: "SupplierSerialID",
                schema: "Inv",
                table: "PurchaseOrder");*/

            migrationBuilder.DropColumn(
                name: "PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN");

        /*    migrationBuilder.RenameColumn(
                name: "SupplierSerialId",
                schema: "Inv",
                table: "PurchaseOrder",
                newName: "SupplierSerialID");*/

/*            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrder_SupplierSerialId",
                schema: "Inv",
                table: "PurchaseOrder",
                newName: "IX_PurchaseOrder_SupplierSerialID");*/

            migrationBuilder.AlterColumn<int>(
                name: "SupplierSerialID",
                schema: "Inv",
                table: "PurchaseOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GRN_POSerialID",
                schema: "Inv",
                table: "GRN",
                column: "POSerialID");

            migrationBuilder.AddForeignKey(
                name: "FK_GRN_PurchaseOrder_POSerialID",
                schema: "Inv",
                table: "GRN",
                column: "POSerialID",
                principalSchema: "Inv",
                principalTable: "PurchaseOrder",
                principalColumn: "POSerialID",
                onDelete: ReferentialAction.Restrict);

/*            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Supplier_SupplierSerialID",
                schema: "Inv",
                table: "PurchaseOrder",
                column: "SupplierSerialID",
                principalSchema: "Inv",
                principalTable: "Supplier",
                principalColumn: "SupplierSerialId",
                onDelete: ReferentialAction.Restrict);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GRN_PurchaseOrder_POSerialID",
                schema: "Inv",
                table: "GRN");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseOrder_Supplier_SupplierSerialID",
                schema: "Inv",
                table: "PurchaseOrder");

            migrationBuilder.DropIndex(
                name: "IX_GRN_POSerialID",
                schema: "Inv",
                table: "GRN");

      /*      migrationBuilder.RenameColumn(
                name: "SupplierSerialID",
                schema: "Inv",
                table: "PurchaseOrder",
                newName: "SupplierSerialId");*/
/*
            migrationBuilder.RenameIndex(
                name: "IX_PurchaseOrder_SupplierSerialID",
                schema: "Inv",
                table: "PurchaseOrder",
                newName: "IX_PurchaseOrder_SupplierSerialId");*/

  /*          migrationBuilder.AlterColumn<int>(
                name: "SupplierSerialId",
                schema: "Inv",
                table: "PurchaseOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");*/

      /*      migrationBuilder.AddColumn<int>(
                name: "SupplierSerialID",
                schema: "Inv",
                table: "PurchaseOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);*/

            migrationBuilder.AddColumn<int>(
                name: "PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GRN_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN",
                column: "PurchaseOrderPOSerialID");

            migrationBuilder.AddForeignKey(
                name: "FK_GRN_PurchaseOrder_PurchaseOrderPOSerialID",
                schema: "Inv",
                table: "GRN",
                column: "PurchaseOrderPOSerialID",
                principalSchema: "Inv",
                principalTable: "PurchaseOrder",
                principalColumn: "POSerialID");

 /*           migrationBuilder.AddForeignKey(
                name: "FK_PurchaseOrder_Supplier_SupplierSerialId",
                schema: "Inv",
                table: "PurchaseOrder",
                column: "SupplierSerialId",
                principalSchema: "Inv",
                principalTable: "Supplier",
                principalColumn: "SupplierSerialId");*/
        }
    }
}
