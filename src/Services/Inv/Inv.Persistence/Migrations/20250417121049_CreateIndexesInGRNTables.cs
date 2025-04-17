using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateIndexesInGRNTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_GRNHeader_ApprovedBy",
                schema: "Inv",
                table: "GRNHeader",
                column: "ApprovedBy",
                filter: "[ApprovedBy] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GRNHeader_ApprovedBy_IsDeleted",
                schema: "Inv",
                table: "GRNHeader",
                columns: new[] { "ApprovedBy", "IsDeleted" },
                filter: "[ApprovedBy] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GRNHeader_CompSerialID",
                schema: "Inv",
                table: "GRNHeader",
                column: "CompSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_GRNHeader_CreatedDate",
                schema: "Inv",
                table: "GRNHeader",
                column: "CreatedDate");

            migrationBuilder.CreateIndex(
                name: "IX_GRNDetail_BatchNumber",
                schema: "Inv",
                table: "GRNDetail",
                column: "BatchNumber",
                filter: "[BatchNumber] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_GRNDetail_GRNHeaderSerialID_ItemSerialID",
                schema: "Inv",
                table: "GRNDetail",
                columns: new[] { "GRNHeaderSerialID", "ItemSerialID" });

            migrationBuilder.CreateIndex(
                name: "IX_GRNDetail_ItemSerialID",
                schema: "Inv",
                table: "GRNDetail",
                column: "ItemSerialID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_GRNHeader_ApprovedBy",
                schema: "Inv",
                table: "GRNHeader");

            migrationBuilder.DropIndex(
                name: "IX_GRNHeader_ApprovedBy_IsDeleted",
                schema: "Inv",
                table: "GRNHeader");

            migrationBuilder.DropIndex(
                name: "IX_GRNHeader_CompSerialID",
                schema: "Inv",
                table: "GRNHeader");

            migrationBuilder.DropIndex(
                name: "IX_GRNHeader_CreatedDate",
                schema: "Inv",
                table: "GRNHeader");

            migrationBuilder.DropIndex(
                name: "IX_GRNDetail_BatchNumber",
                schema: "Inv",
                table: "GRNDetail");

            migrationBuilder.DropIndex(
                name: "IX_GRNDetail_GRNHeaderSerialID_ItemSerialID",
                schema: "Inv",
                table: "GRNDetail");

            migrationBuilder.DropIndex(
                name: "IX_GRNDetail_ItemSerialID",
                schema: "Inv",
                table: "GRNDetail");
        }
    }
}
