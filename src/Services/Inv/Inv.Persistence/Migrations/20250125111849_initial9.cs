using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial9 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parameter",
                schema: "Inv");

            /*migrationBuilder.CreateTable(
                name: "TheNumbers",
                schema: "Inv",
                columns: table => new
                {
                    TheNumberSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TheNumberID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.TheNumberID"),
                    TheNumberName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    ComSerialID = table.Column<int>(type: "int", nullable: true),
                    LastNumber = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TheNumbers", x => x.TheNumberSerialID);
                });*/
            /*
            migrationBuilder.CreateIndex(
                name: "IX_UOMConversion_UOMToID",
                schema: "Inv",
                table: "UOMConversion",
                column: "UOMToID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_BrandSerialID",
                schema: "Inv",
                table: "Item",
                column: "BrandSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemTypeSerialID",
                schema: "Inv",
                table: "Item",
                column: "ItemTypeSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_MainCategorySerialID",
                schema: "Inv",
                table: "Item",
                column: "MainCategorySerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ModelSerialID",
                schema: "Inv",
                table: "Item",
                column: "ModelSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_SubCategorySerialID",
                schema: "Inv",
                table: "Item",
                column: "SubCategorySerialID");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Brand_BrandSerialID",
                schema: "Inv",
                table: "Item",
                column: "BrandSerialID",
                principalSchema: "Inv",
                principalTable: "Brand",
                principalColumn: "BrandSerialID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_ItemType_ItemTypeSerialID",
                schema: "Inv",
                table: "Item",
                column: "ItemTypeSerialID",
                principalSchema: "Inv",
                principalTable: "ItemType",
                principalColumn: "ItemTypeSerialID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_MainCategory_MainCategorySerialID",
                schema: "Inv",
                table: "Item",
                column: "MainCategorySerialID",
                principalSchema: "Inv",
                principalTable: "MainCategory",
                principalColumn: "MainCategorySerialID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Model_ModelSerialID",
                schema: "Inv",
                table: "Item",
                column: "ModelSerialID",
                principalSchema: "Inv",
                principalTable: "Model",
                principalColumn: "ModelSerialID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_SubCategory_SubCategorySerialID",
                schema: "Inv",
                table: "Item",
                column: "SubCategorySerialID",
                principalSchema: "Inv",
                principalTable: "SubCategory",
                principalColumn: "SubCategorySerialID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UOMConversion_UOM_UOMToID",
                schema: "Inv",
                table: "UOMConversion",
                column: "UOMToID",
                principalSchema: "Inv",
                principalTable: "UOM",
                principalColumn: "UOMSerialID",
                onDelete: ReferentialAction.Cascade);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Brand_BrandSerialID",
                schema: "Inv",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_ItemType_ItemTypeSerialID",
                schema: "Inv",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_MainCategory_MainCategorySerialID",
                schema: "Inv",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Model_ModelSerialID",
                schema: "Inv",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_SubCategory_SubCategorySerialID",
                schema: "Inv",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_UOMConversion_UOM_UOMToID",
                schema: "Inv",
                table: "UOMConversion");
            */
            migrationBuilder.DropTable(
                name: "TheNumbers",
                schema: "Inv");
            /*
            migrationBuilder.DropIndex(
                name: "IX_UOMConversion_UOMToID",
                schema: "Inv",
                table: "UOMConversion");

            migrationBuilder.DropIndex(
                name: "IX_Item_BrandSerialID",
                schema: "Inv",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ItemTypeSerialID",
                schema: "Inv",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_MainCategorySerialID",
                schema: "Inv",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_ModelSerialID",
                schema: "Inv",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_SubCategorySerialID",
                schema: "Inv",
                table: "Item");
            */
            migrationBuilder.CreateTable(
                name: "Parameter",
                schema: "Inv",
                columns: table => new
                {
                    ParamSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastID = table.Column<int>(type: "int", nullable: true),
                    Length = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ParamID = table.Column<int>(type: "int", nullable: false),
                    ParamName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.ParamSerialID);
                });
        }
    }
}
