using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddSystemPOTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SystemPOHeader",
                schema: "Inv",
                columns: table => new
                {
                    SystemPOHeaderSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemPOID = table.Column<int>(type: "int", nullable: false),
                    CompSerialID = table.Column<int>(type: "int", nullable: false),
                    SupplierSerialID = table.Column<int>(type: "int", nullable: false),
                    BillingLocationSerialID = table.Column<int>(type: "int", nullable: false),
                    PayTermSerialID = table.Column<int>(type: "int", nullable: false),
                    IncotermSerialID = table.Column<int>(type: "int", nullable: false),
                    ShipMode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
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
                    table.PrimaryKey("PK_SystemPOHeader", x => x.SystemPOHeaderSerialID);
                });

            migrationBuilder.CreateTable(
                name: "SystemPODetail",
                schema: "Inv",
                columns: table => new
                {
                    SystemPODetailSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SystemPOHeaderSerialID = table.Column<int>(type: "int", nullable: false),
                    LineNumber = table.Column<int>(type: "int", nullable: false),
                    ItemSerialID = table.Column<int>(type: "int", nullable: false),
                    UOMSerialID = table.Column<int>(type: "int", nullable: false),
                    Qty = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BalToReceive = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SystemPODetail", x => x.SystemPODetailSerialID);
                    table.ForeignKey(
                        name: "FK_SystemPODetail_SystemPOHeader_SystemPOHeaderSerialID",
                        column: x => x.SystemPOHeaderSerialID,
                        principalSchema: "Inv",
                        principalTable: "SystemPOHeader",
                        principalColumn: "SystemPOHeaderSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SystemPODetail_IsDeleted",
                schema: "Inv",
                table: "SystemPODetail",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_SystemPODetail_SystemPOHeaderSerialID",
                schema: "Inv",
                table: "SystemPODetail",
                column: "SystemPOHeaderSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_SystemPOHeader_IsDeleted",
                schema: "Inv",
                table: "SystemPOHeader",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SystemPODetail",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "SystemPOHeader",
                schema: "Inv");
        }
    }
}
