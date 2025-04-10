using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           /*migrationBuilder.CreateSequence<int>(
                name: "BinLctnID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "RackID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "StoreID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "WHID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "ZoneID",
                schema: "dbo");

            migrationBuilder.AlterColumn<int>(
                name: "ItemTypeSerialID",
                schema: "Inv",
                table: "ItemType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "10, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialFeatures",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemPartNo",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemDes",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ItemCode",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Guage",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Construction",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Article",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Warehouse",
                schema: "Inv",
                columns: table => new
                {
                    WHSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WHID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.WHID"),
                    WHName = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    Address2 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    Address3 = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    ComSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouse", x => x.WHSerialID);
                });

            migrationBuilder.CreateTable(
                name: "Store",
                schema: "Inv",
                columns: table => new
                {
                    StoreSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.StoreID"),
                    ComSerialID = table.Column<int>(type: "int", nullable: false),
                    StoreName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    WHSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.StoreSerialID);
                    table.ForeignKey(
                        name: "FK_Store_Warehouse_WHSerialID",
                        column: x => x.WHSerialID,
                        principalSchema: "Inv",
                        principalTable: "Warehouse",
                        principalColumn: "WHSerialID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                schema: "Inv",
                columns: table => new
                {
                    ZoneSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.ZoneID"),
                    ZoneName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    ComSerialID = table.Column<int>(type: "int", nullable: false),
                    WHSerialID = table.Column<int>(type: "int", nullable: false),
                    StoreSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.ZoneSerialID);
                    table.ForeignKey(
                        name: "FK_Zone_Store_StoreSerialID",
                        column: x => x.StoreSerialID,
                        principalSchema: "Inv",
                        principalTable: "Store",
                        principalColumn: "StoreSerialID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zone_Warehouse_WHSerialID",
                        column: x => x.WHSerialID,
                        principalSchema: "Inv",
                        principalTable: "Warehouse",
                        principalColumn: "WHSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rack",
                schema: "Inv",
                columns: table => new
                {
                    RackSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RackID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.RackID"),
                    RackName = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    RackCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Rows = table.Column<int>(type: "int", nullable: false),
                    Columns = table.Column<int>(type: "int", nullable: false),
                    Compartments = table.Column<int>(type: "int", nullable: true),
                    ComSerialID = table.Column<int>(type: "int", nullable: false),
                    WHSerialID = table.Column<int>(type: "int", nullable: false),
                    StoreSerialID = table.Column<int>(type: "int", nullable: false),
                    ZoneSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rack", x => x.RackSerialID);
                    table.ForeignKey(
                        name: "FK_Rack_Store_StoreSerialID",
                        column: x => x.StoreSerialID,
                        principalSchema: "Inv",
                        principalTable: "Store",
                        principalColumn: "StoreSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rack_Warehouse_WHSerialID",
                        column: x => x.WHSerialID,
                        principalSchema: "Inv",
                        principalTable: "Warehouse",
                        principalColumn: "WHSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rack_Zone_ZoneSerialID",
                        column: x => x.ZoneSerialID,
                        principalSchema: "Inv",
                        principalTable: "Zone",
                        principalColumn: "ZoneSerialID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BinLocation",
                schema: "Inv",
                columns: table => new
                {
                    BinLctnSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BinLctnID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.BinLctnID"),
                    ItemSerialID = table.Column<int>(type: "int", nullable: false),
                    Row = table.Column<int>(type: "int", nullable: false),
                    Column = table.Column<int>(type: "int", nullable: false),
                    Compartment = table.Column<int>(type: "int", nullable: true),
                    ComSerialID = table.Column<int>(type: "int", nullable: false),
                    WHSerialID = table.Column<int>(type: "int", nullable: false),
                    StoreSerialID = table.Column<int>(type: "int", nullable: false),
                    ZoneSerialID = table.Column<int>(type: "int", nullable: false),
                    RackSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BinLocation", x => x.BinLctnSerialID);
                    table.ForeignKey(
                        name: "FK_BinLocation_Rack_RackSerialID",
                        column: x => x.RackSerialID,
                        principalSchema: "Inv",
                        principalTable: "Rack",
                        principalColumn: "RackSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BinLocation_Store_StoreSerialID",
                        column: x => x.StoreSerialID,
                        principalSchema: "Inv",
                        principalTable: "Store",
                        principalColumn: "StoreSerialID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BinLocation_Warehouse_WHSerialID",
                        column: x => x.WHSerialID,
                        principalSchema: "Inv",
                        principalTable: "Warehouse",
                        principalColumn: "WHSerialID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BinLocation_Zone_ZoneSerialID",
                        column: x => x.ZoneSerialID,
                        principalSchema: "Inv",
                        principalTable: "Zone",
                        principalColumn: "ZoneSerialID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BinLocation_BinLctnID",
                schema: "Inv",
                table: "BinLocation",
                column: "BinLctnID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BinLocation_IsDeleted",
                schema: "Inv",
                table: "BinLocation",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_BinLocation_RackSerialID",
                schema: "Inv",
                table: "BinLocation",
                column: "RackSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_BinLocation_StoreSerialID",
                schema: "Inv",
                table: "BinLocation",
                column: "StoreSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_BinLocation_WHSerialID",
                schema: "Inv",
                table: "BinLocation",
                column: "WHSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_BinLocation_ZoneSerialID",
                schema: "Inv",
                table: "BinLocation",
                column: "ZoneSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Rack_IsDeleted",
                schema: "Inv",
                table: "Rack",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Rack_RackID",
                schema: "Inv",
                table: "Rack",
                column: "RackID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rack_StoreSerialID",
                schema: "Inv",
                table: "Rack",
                column: "StoreSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Rack_WHSerialID",
                schema: "Inv",
                table: "Rack",
                column: "WHSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Rack_ZoneSerialID",
                schema: "Inv",
                table: "Rack",
                column: "ZoneSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Store_IsDeleted",
                schema: "Inv",
                table: "Store",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Store_StoreID",
                schema: "Inv",
                table: "Store",
                column: "StoreID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Store_WHSerialID",
                schema: "Inv",
                table: "Store",
                column: "WHSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_IsDeleted",
                schema: "Inv",
                table: "Warehouse",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_WHID",
                schema: "Inv",
                table: "Warehouse",
                column: "WHID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouse_WHSerialID",
                schema: "Inv",
                table: "Warehouse",
                column: "WHSerialID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Zone_IsDeleted",
                schema: "Inv",
                table: "Zone",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_StoreSerialID",
                schema: "Inv",
                table: "Zone",
                column: "StoreSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_WHSerialID",
                schema: "Inv",
                table: "Zone",
                column: "WHSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_ZoneID",
                schema: "Inv",
                table: "Zone",
                column: "ZoneID",
                unique: true);*/
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.DropTable(
                name: "BinLocation",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "Rack",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "Zone",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "Store",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "Warehouse",
                schema: "Inv");

            migrationBuilder.DropSequence(
                name: "BinLctnID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "RackID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "StoreID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "WHID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "ZoneID",
                schema: "dbo");

            migrationBuilder.AlterColumn<int>(
                name: "ItemTypeSerialID",
                schema: "Inv",
                table: "ItemType",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "10, 1");

            migrationBuilder.AlterColumn<string>(
                name: "SpecialFeatures",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemPartNo",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ItemDes",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ItemCode",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Guage",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Construction",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Article",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);*/
        }
    }
}
