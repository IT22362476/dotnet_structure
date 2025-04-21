using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddDelRecordsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "DelRecID",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "DelRecord",
                schema: "Inv",
                columns: table => new
                {
                    DelRecSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DelRecID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.DelRecID"),
                    DocTable = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DocSerialID = table.Column<int>(type: "int", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DelRecord", x => x.DelRecSerialID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DelRecord_DelRecID",
                schema: "Inv",
                table: "DelRecord",
                column: "DelRecID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DelRecord_IsDeleted",
                schema: "Inv",
                table: "DelRecord",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DelRecord",
                schema: "Inv");

            migrationBuilder.DropSequence(
                name: "DelRecID",
                schema: "dbo");
        }
    }
}
