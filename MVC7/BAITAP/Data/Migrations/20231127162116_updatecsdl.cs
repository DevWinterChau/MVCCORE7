using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAITAP.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatecsdl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "SoLuong",
                table: "MATHANG",
                type: "int",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(short),
                oldType: "smallint",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<int>(
                name: "GiaGoc",
                table: "MATHANG",
                type: "int",
                nullable: false,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValueSql: "((0))");

            migrationBuilder.AddColumn<string>(
                name: "Chatlieu",
                table: "MATHANG",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Donvitinh",
                table: "MATHANG",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Iskhuyemmai",
                table: "MATHANG",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Kichco",
                table: "MATHANG",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MaMHCHINH",
                table: "MATHANG",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Mausac",
                table: "MATHANG",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Ngaycapnhat",
                table: "MATHANG",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Ngaytao",
                table: "MATHANG",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "trangthai",
                table: "MATHANG",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Xaphuong",
                table: "HOADON",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Tinh",
                table: "HOADON",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Tennguoinhan",
                table: "HOADON",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Sodienthoai",
                table: "HOADON",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Quanhuyen",
                table: "HOADON",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "Diachi",
                table: "HOADON",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80);

            migrationBuilder.CreateTable(
                name: "LoaiKhuyenMai",
                columns: table => new
                {
                    MaLoaiKM = table.Column<int>(type: "int", nullable: false),
                    TenLoaiKM = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__LoaiKhuy__12250B73F703077B", x => x.MaLoaiKM);
                });

            migrationBuilder.CreateTable(
                name: "MH",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TEN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HINHANH = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "('Chua có ?nh')"),
                    NGAYTAO = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    NGAYCAPNHAT = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MH__3214EC27B1017620", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CT_KhuyenMai",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    TenKM = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    MoTa = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    NgayBatDau = table.Column<DateTime>(type: "date", nullable: false),
                    NgayKetThuc = table.Column<DateTime>(type: "date", nullable: false),
                    Soluongmuatoithieu = table.Column<int>(type: "int", nullable: false),
                    Sotienmuatoithieu = table.Column<int>(type: "int", nullable: false),
                    PhanTramGiamGia = table.Column<int>(type: "int", nullable: false),
                    GiaGiam = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    DieuKienApDung = table.Column<decimal>(type: "decimal(10,2)", nullable: false),
                    Soluongsudung = table.Column<int>(type: "int", nullable: false),
                    NhomSPKhuyemai = table.Column<int>(type: "int", nullable: true),
                    TrangThai = table.Column<bool>(type: "bit", nullable: false),
                    MaLoaiKM = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CT_Khuye__3214EC2766AA2C03", x => x.ID);
                    table.ForeignKey(
                        name: "FK_KhuyenMai_LoaiKhuyenMai",
                        column: x => x.MaLoaiKM,
                        principalTable: "LoaiKhuyenMai",
                        principalColumn: "MaLoaiKM",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KhuyenMai_NhomSP",
                        column: x => x.NhomSPKhuyemai,
                        principalTable: "DANHMUC",
                        principalColumn: "MaDM");
                });

            migrationBuilder.CreateTable(
                name: "CT_KhuyenMai_SanPham",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    MaCTKM = table.Column<int>(type: "int", nullable: false),
                    mamh = table.Column<int>(type: "int", nullable: false),
                    Phantramkhuyenmai = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CT_Khuye__3214EC2764208017", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CTKM_KhuyenMai",
                        column: x => x.MaCTKM,
                        principalTable: "CT_KhuyenMai",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CTKM_SanPham",
                        column: x => x.mamh,
                        principalTable: "MATHANG",
                        principalColumn: "MaMH",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CT_KhuyenMai_MaLoaiKM",
                table: "CT_KhuyenMai",
                column: "MaLoaiKM");

            migrationBuilder.CreateIndex(
                name: "IX_CT_KhuyenMai_NhomSPKhuyemai",
                table: "CT_KhuyenMai",
                column: "NhomSPKhuyemai");

            migrationBuilder.CreateIndex(
                name: "IX_CT_KhuyenMai_SanPham_MaCTKM",
                table: "CT_KhuyenMai_SanPham",
                column: "MaCTKM");

            migrationBuilder.CreateIndex(
                name: "IX_CT_KhuyenMai_SanPham_mamh",
                table: "CT_KhuyenMai_SanPham",
                column: "mamh");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CT_KhuyenMai_SanPham");

            migrationBuilder.DropTable(
                name: "MH");

            migrationBuilder.DropTable(
                name: "CT_KhuyenMai");

            migrationBuilder.DropTable(
                name: "LoaiKhuyenMai");

            migrationBuilder.DropColumn(
                name: "Chatlieu",
                table: "MATHANG");

            migrationBuilder.DropColumn(
                name: "Donvitinh",
                table: "MATHANG");

            migrationBuilder.DropColumn(
                name: "Iskhuyemmai",
                table: "MATHANG");

            migrationBuilder.DropColumn(
                name: "Kichco",
                table: "MATHANG");

            migrationBuilder.DropColumn(
                name: "MaMHCHINH",
                table: "MATHANG");

            migrationBuilder.DropColumn(
                name: "Mausac",
                table: "MATHANG");

            migrationBuilder.DropColumn(
                name: "Ngaycapnhat",
                table: "MATHANG");

            migrationBuilder.DropColumn(
                name: "Ngaytao",
                table: "MATHANG");

            migrationBuilder.DropColumn(
                name: "trangthai",
                table: "MATHANG");

            migrationBuilder.AlterColumn<short>(
                name: "SoLuong",
                table: "MATHANG",
                type: "smallint",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<int>(
                name: "GiaGoc",
                table: "MATHANG",
                type: "int",
                nullable: true,
                defaultValueSql: "((0))",
                oldClrType: typeof(int),
                oldType: "int",
                oldDefaultValueSql: "((0))");

            migrationBuilder.AlterColumn<string>(
                name: "Xaphuong",
                table: "HOADON",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tinh",
                table: "HOADON",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tennguoinhan",
                table: "HOADON",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sodienthoai",
                table: "HOADON",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Quanhuyen",
                table: "HOADON",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Diachi",
                table: "HOADON",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(80)",
                oldMaxLength: 80,
                oldNullable: true);
        }
    }
}
