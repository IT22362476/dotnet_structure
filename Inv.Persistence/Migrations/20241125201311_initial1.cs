using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Inv.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class initial1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UOMSerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "SubCategorySerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Remarks",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ModelSerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MainCategorySerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ItemTypeSerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "ItemCode",
                schema: "Inv",
                table: "Item",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BrandSerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

       /*     migrationBuilder.AlterColumn<int>(
                name: "BITSerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");*/

            migrationBuilder.RestartSequence(
                name: "ItemTypeID",
                schema: "dbo",
                startValue: 10L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UOMSerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SubCategorySerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Size",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "Remarks",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ModelSerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MainCategorySerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemTypeSerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ItemCode",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "BrandSerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
/*
            migrationBuilder.AlterColumn<int>(
                name: "BITSerialID",
                schema: "Inv",
                table: "Item",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);*/

            migrationBuilder.RestartSequence(
                name: "ItemTypeID",
                schema: "dbo",
                startValue: 1L);
        }
    }
}
