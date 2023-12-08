using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAITAP.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedcactruongdulieu : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GiaBan",
                table: "MATHANG",
                type: "int",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AddColumn<string>(
                name: "Diachi",
                table: "HOADON",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Quanhuyen",
                table: "HOADON",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sodienthoai",
                table: "HOADON",
                type: "nvarchar(20)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tennguoinhan",
                table: "HOADON",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tinh",
                table: "HOADON",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Xaphuong",
                table: "HOADON",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "SOLUONG",
                table: "CTHOADON",
                type: "int",
                nullable: true,
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Diachi",
                table: "HOADON");

            migrationBuilder.DropColumn(
                name: "Quanhuyen",
                table: "HOADON");

            migrationBuilder.DropColumn(
                name: "Sodienthoai",
                table: "HOADON");

            migrationBuilder.DropColumn(
                name: "Tennguoinhan",
                table: "HOADON");

            migrationBuilder.DropColumn(
                name: "Tinh",
                table: "HOADON");

            migrationBuilder.DropColumn(
                name: "Xaphuong",
                table: "HOADON");

            migrationBuilder.AlterColumn<int>(
                name: "GiaBan",
                table: "MATHANG",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<short>(
                name: "SOLUONG",
                table: "CTHOADON",
                type: "smallint",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
