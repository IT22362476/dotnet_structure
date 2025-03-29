using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateSequence<int>(
                name: "CompatibleItemlID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "CusPriceCatID",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "CompatibleItem",
                schema: "Inv",
                columns: table => new
                {
                    CompatibleItemSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompatibleItemlID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.CompatibleItemlID"),
                    ItemSerialID = table.Column<int>(type: "int", nullable: false),
                    ItemCompatibleSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompatibleItem", x => x.CompatibleItemSerialID);
                    table.ForeignKey(
                        name: "FK_CompatibleItem_Item_ItemCompatibleSerialID",
                        column: x => x.ItemCompatibleSerialID,
                        principalSchema: "Inv",
                        principalTable: "Item",
                        principalColumn: "ItemSerialID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CompatibleItem_Item_ItemSerialID",
                        column: x => x.ItemSerialID,
                        principalSchema: "Inv",
                        principalTable: "Item",
                        principalColumn: "ItemSerialID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CusPriceCategory",
                schema: "Inv",
                columns: table => new
                {
                    CusPriceCatSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CusPriceCatID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.CusPriceCatID"),
                    CusPriceCatName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CusPriceCategory", x => x.CusPriceCatSerialID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompatibleItem_CompatibleItemlID",
                schema: "Inv",
                table: "CompatibleItem",
                column: "CompatibleItemlID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompatibleItem_IsDeleted",
                schema: "Inv",
                table: "CompatibleItem",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_CompatibleItem_ItemCompatibleSerialID",
                schema: "Inv",
                table: "CompatibleItem",
                column: "ItemCompatibleSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_CompatibleItem_ItemSerialID",
                schema: "Inv",
                table: "CompatibleItem",
                column: "ItemSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_CusPriceCategory_CusPriceCatID",
                schema: "Inv",
                table: "CusPriceCategory",
                column: "CusPriceCatID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CusPriceCategory_IsDeleted",
                schema: "Inv",
                table: "CusPriceCategory",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompatibleItem",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "CusPriceCategory",
                schema: "Inv");

            migrationBuilder.DropSequence(
                name: "CompatibleItemlID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "CusPriceCatID",
                schema: "dbo");
        }
    }
}
