using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BAITAP.Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CHUCVU",
                columns: table => new
                {
                    MACV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TEN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    HESO = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CHUCVU__603F18344188813A", x => x.MACV);
                });

            migrationBuilder.CreateTable(
                name: "DANHMUC",
                columns: table => new
                {
                    MaDM = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DANHMUC__2725866E7D416BF5", x => x.MaDM);
                });

            migrationBuilder.CreateTable(
                name: "KHACHHANG",
                columns: table => new
                {
                    MaKH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    DIENTHOAI = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    EMAIL = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MATKHAU = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__KHACHHAN__2725CF1E4FFE67E7", x => x.MaKH);
                });

            migrationBuilder.CreateTable(
                name: "NHANVIEN",
                columns: table => new
                {
                    MANV = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TEN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MACV = table.Column<int>(type: "int", nullable: false),
                    DIENTHOAI = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    EMAIL = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MATKHAU = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__NHANVIEN__603F51144B34CABD", x => x.MANV);
                    table.ForeignKey(
                        name: "FK__NHANVIEN__MACV__3E52440B",
                        column: x => x.MACV,
                        principalTable: "CHUCVU",
                        principalColumn: "MACV");
                });

            migrationBuilder.CreateTable(
                name: "MATHANG",
                columns: table => new
                {
                    MaMH = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ten = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    GiaGoc = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    GiaBan = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    SoLuong = table.Column<short>(type: "smallint", nullable: true, defaultValueSql: "((0))"),
                    MoTa = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    HinhAnh = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    MaDM = table.Column<int>(type: "int", nullable: false),
                    LuotXem = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))"),
                    LuotMua = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__MATHANG__2725DFD9A470EBC7", x => x.MaMH);
                    table.ForeignKey(
                        name: "FK__MATHANG__MaDM__3D5E1FD2",
                        column: x => x.MaDM,
                        principalTable: "DANHMUC",
                        principalColumn: "MaDM");
                });

            migrationBuilder.CreateTable(
                name: "DIACHI",
                columns: table => new
                {
                    MaDC = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MAKH = table.Column<int>(type: "int", nullable: false),
                    DIACHI = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PHUONGXA = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    QUANHUYEN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TINHTHANH = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MACDINH = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__DIACHI__2725866496086918", x => x.MaDC);
                    table.ForeignKey(
                        name: "FK__DIACHI__MAKH__3B75D760",
                        column: x => x.MAKH,
                        principalTable: "KHACHHANG",
                        principalColumn: "MaKH");
                });

            migrationBuilder.CreateTable(
                name: "HOADON",
                columns: table => new
                {
                    MAHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MAKH = table.Column<int>(type: "int", nullable: false),
                    NGAY = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    TONGTIEN = table.Column<int>(type: "int", nullable: false),
                    TRANGTHAI = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__HOADON__603F20CE173D75A8", x => x.MAHD);
                    table.ForeignKey(
                        name: "FK__HOADON__MAKH__3C69FB99",
                        column: x => x.MAKH,
                        principalTable: "KHACHHANG",
                        principalColumn: "MaKH");
                });

            migrationBuilder.CreateTable(
                name: "CTHOADON",
                columns: table => new
                {
                    MACTHD = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MAHD = table.Column<int>(type: "int", nullable: false),
                    MAMH = table.Column<int>(type: "int", nullable: false),
                    DONGIA = table.Column<int>(type: "int", nullable: true),
                    SOLUONG = table.Column<short>(type: "smallint", nullable: true),
                    THANHTIEN = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CTHOADON__F50CB4CB4C99E6E6", x => x.MACTHD);
                    table.ForeignKey(
                        name: "FK__CTHOADON__MAHD__398D8EEE",
                        column: x => x.MAHD,
                        principalTable: "HOADON",
                        principalColumn: "MAHD");
                    table.ForeignKey(
                        name: "FK__CTHOADON__MAMH__3A81B327",
                        column: x => x.MAMH,
                        principalTable: "MATHANG",
                        principalColumn: "MaMH");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CTHOADON_MAHD",
                table: "CTHOADON",
                column: "MAHD");

            migrationBuilder.CreateIndex(
                name: "IX_CTHOADON_MAMH",
                table: "CTHOADON",
                column: "MAMH");

            migrationBuilder.CreateIndex(
                name: "IX_DIACHI_MAKH",
                table: "DIACHI",
                column: "MAKH");

            migrationBuilder.CreateIndex(
                name: "IX_HOADON_MAKH",
                table: "HOADON",
                column: "MAKH");

            migrationBuilder.CreateIndex(
                name: "IX_MATHANG_MaDM",
                table: "MATHANG",
                column: "MaDM");

            migrationBuilder.CreateIndex(
                name: "IX_NHANVIEN_MACV",
                table: "NHANVIEN",
                column: "MACV");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CTHOADON");

            migrationBuilder.DropTable(
                name: "DIACHI");

            migrationBuilder.DropTable(
                name: "NHANVIEN");

            migrationBuilder.DropTable(
                name: "HOADON");

            migrationBuilder.DropTable(
                name: "MATHANG");

            migrationBuilder.DropTable(
                name: "CHUCVU");

            migrationBuilder.DropTable(
                name: "KHACHHANG");

            migrationBuilder.DropTable(
                name: "DANHMUC");
        }
    }
}
