using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Inv");

            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "BITID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "BrandID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "ItemID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "ItemTypeID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "MainCategoryID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "ModelID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "SubCategoryID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "UOMConvID",
                schema: "dbo");

            migrationBuilder.CreateSequence<int>(
                name: "UOMID",
                schema: "dbo");

            migrationBuilder.CreateTable(
                name: "AuditTrail",
                schema: "Inv",
                columns: table => new
                {
                    AudtTralID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuditDateTimeUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserSerialID = table.Column<int>(type: "int", nullable: false),
                    FrmSerialID = table.Column<int>(type: "int", nullable: false),
                    LoginLogSerialID = table.Column<long>(type: "bigint", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AuditData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditTrail", x => x.AudtTralID);
                });

            migrationBuilder.CreateTable(
                name: "Brand",
                schema: "Inv",
                columns: table => new
                {
                    BrandSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrandID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.BrandID"),
                    BrandName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.BrandSerialID);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                schema: "Inv",
                columns: table => new
                {
                    ItemSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.ItemID"),
                    ItemCode = table.Column<int>(type: "int", nullable: false),
                    ItemTypeSerialID = table.Column<int>(type: "int", nullable: false),
                    ItemDes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MainCategorySerialID = table.Column<int>(type: "int", nullable: false),
                    SubCategorySerialID = table.Column<int>(type: "int", nullable: false),
                    BITSerialID = table.Column<int>(type: "int", nullable: false),
                    ModelSerialID = table.Column<int>(type: "int", nullable: false),
                    BrandSerialID = table.Column<int>(type: "int", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    Volume = table.Column<double>(type: "float", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemPartNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Article = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remarks = table.Column<int>(type: "int", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Guage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Construction = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SpecialFeatures = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UOMSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemSerialID);
                });

            migrationBuilder.CreateTable(
                name: "ItemType",
                schema: "Inv",
                columns: table => new
                {
                    ItemTypeSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemTypeID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.ItemTypeID"),
                    ItemTypeName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemType", x => x.ItemTypeSerialID);
                });

            migrationBuilder.CreateTable(
                name: "MainCategory",
                schema: "Inv",
                columns: table => new
                {
                    MainCategorySerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MainCategoryID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.MainCategoryID"),
                    MainCategoryName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainCategory", x => x.MainCategorySerialID);
                });

            migrationBuilder.CreateTable(
                name: "Parameter",
                schema: "Inv",
                columns: table => new
                {
                    ParamSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParamID = table.Column<int>(type: "int", nullable: false),
                    ParamName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Length = table.Column<int>(type: "int", maxLength: 5, nullable: false),
                    LastID = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameter", x => x.ParamSerialID);
                });

            migrationBuilder.CreateTable(
                name: "UOM",
                schema: "Inv",
                columns: table => new
                {
                    UOMSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UOMID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.UOMID"),
                    UOMName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    UOMDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UOM", x => x.UOMSerialID);
                });

            migrationBuilder.CreateTable(
                name: "Model",
                schema: "Inv",
                columns: table => new
                {
                    ModelSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.ModelID"),
                    ModelName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    BrandSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.ModelSerialID);
                    table.ForeignKey(
                        name: "FK_Model_Brand_BrandSerialID",
                        column: x => x.BrandSerialID,
                        principalSchema: "Inv",
                        principalTable: "Brand",
                        principalColumn: "BrandSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BrandItemType",
                schema: "Inv",
                columns: table => new
                {
                    BITSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BITID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.BITID"),
                    BrandSerialID = table.Column<int>(type: "int", nullable: false),
                    ItemTypeSerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandItemType", x => x.BITSerialID);
                    table.ForeignKey(
                        name: "FK_BrandItemType_Brand_BrandSerialID",
                        column: x => x.BrandSerialID,
                        principalSchema: "Inv",
                        principalTable: "Brand",
                        principalColumn: "BrandSerialID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BrandItemType_ItemType_ItemTypeSerialID",
                        column: x => x.ItemTypeSerialID,
                        principalSchema: "Inv",
                        principalTable: "ItemType",
                        principalColumn: "ItemTypeSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubCategory",
                schema: "Inv",
                columns: table => new
                {
                    SubCategorySerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubCategoryID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.SubCategoryID"),
                    SubCategoryName = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    MainCategorySerialID = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategory", x => x.SubCategorySerialID);
                    table.ForeignKey(
                        name: "FK_SubCategory_MainCategory_MainCategorySerialID",
                        column: x => x.MainCategorySerialID,
                        principalSchema: "Inv",
                        principalTable: "MainCategory",
                        principalColumn: "MainCategorySerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UOMConversion",
                schema: "Inv",
                columns: table => new
                {
                    UOMConvSerialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UOMConvID = table.Column<int>(type: "int", nullable: false, defaultValueSql: "NEXT VALUE FOR dbo.UOMConvID"),
                    UOMSerialID = table.Column<int>(type: "int", nullable: false),
                    UOMToID = table.Column<int>(type: "int", nullable: false),
                    ConversionRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ConversionDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UOMConversion", x => x.UOMConvSerialID);
                    table.ForeignKey(
                        name: "FK_UOMConversion_UOM_UOMSerialID",
                        column: x => x.UOMSerialID,
                        principalSchema: "Inv",
                        principalTable: "UOM",
                        principalColumn: "UOMSerialID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Brand_BrandID",
                schema: "Inv",
                table: "Brand",
                column: "BrandID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Brand_IsDeleted",
                schema: "Inv",
                table: "Brand",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_BrandItemType_BITID",
                schema: "Inv",
                table: "BrandItemType",
                column: "BITID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrandItemType_BrandSerialID",
                schema: "Inv",
                table: "BrandItemType",
                column: "BrandSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_BrandItemType_IsDeleted",
                schema: "Inv",
                table: "BrandItemType",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_BrandItemType_ItemTypeSerialID",
                schema: "Inv",
                table: "BrandItemType",
                column: "ItemTypeSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_IsDeleted",
                schema: "Inv",
                table: "Item",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Item_ItemID",
                schema: "Inv",
                table: "Item",
                column: "ItemID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemType_IsDeleted",
                schema: "Inv",
                table: "ItemType",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_ItemType_ItemTypeID",
                schema: "Inv",
                table: "ItemType",
                column: "ItemTypeID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MainCategory_IsDeleted",
                schema: "Inv",
                table: "MainCategory",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_MainCategory_MainCategoryID",
                schema: "Inv",
                table: "MainCategory",
                column: "MainCategoryID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Model_BrandSerialID",
                schema: "Inv",
                table: "Model",
                column: "BrandSerialID");

            migrationBuilder.CreateIndex(
                name: "IX_Model_IsDeleted",
                schema: "Inv",
                table: "Model",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_Model_ModelID",
                schema: "Inv",
                table: "Model",
                column: "ModelID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_IsDeleted",
                schema: "Inv",
                table: "SubCategory",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_MainCategorySerialID",
                schema: "Inv",
                table: "SubCategory",
                column: "MainCategorySerialID");

            migrationBuilder.CreateIndex(
                name: "IX_SubCategory_SubCategoryID",
                schema: "Inv",
                table: "SubCategory",
                column: "SubCategoryID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UOM_IsDeleted",
                schema: "Inv",
                table: "UOM",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_UOM_UOMID",
                schema: "Inv",
                table: "UOM",
                column: "UOMID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UOMConversion_IsDeleted",
                schema: "Inv",
                table: "UOMConversion",
                column: "IsDeleted",
                filter: "[IsDeleted] = 1");

            migrationBuilder.CreateIndex(
                name: "IX_UOMConversion_UOMConvID",
                schema: "Inv",
                table: "UOMConversion",
                column: "UOMConvID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UOMConversion_UOMSerialID",
                schema: "Inv",
                table: "UOMConversion",
                column: "UOMSerialID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuditTrail",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "BrandItemType",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "Item",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "Model",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "Parameter",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "SubCategory",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "UOMConversion",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "ItemType",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "Brand",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "MainCategory",
                schema: "Inv");

            migrationBuilder.DropTable(
                name: "UOM",
                schema: "Inv");

            migrationBuilder.DropSequence(
                name: "BITID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "BrandID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "ItemID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "ItemTypeID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "MainCategoryID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "ModelID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "SubCategoryID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "UOMConvID",
                schema: "dbo");

            migrationBuilder.DropSequence(
                name: "UOMID",
                schema: "dbo");
        }
    }
}
