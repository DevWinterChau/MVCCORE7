using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Mathang
{
    [DisplayName("Mã sản phẩm")]

    public int MaMh { get; set; }
    [DisplayName("Tên phiên bản sản phẩm")]
    public string Ten { get; set; } = null!;
    [DisplayName("Giá gốc")]
    public int GiaGoc { get; set; }
    [DisplayName("Giá bán")]
    public int GiaBan { get; set; }
    [DisplayName("Số lượng")]

    public int SoLuong { get; set; }
    [DisplayName("Mô tả")]

    public string MoTa { get; set; }
    [DisplayName("Hình ảnh")]

    public string HinhAnh { get; set; }
    [DisplayName("Danh mục")]
    public int MaDm { get; set; }
    [DisplayName("Lượt xem")]

    public int? LuotXem { get; set; }
    [DisplayName("Lượt mua")]

    public int LuotMua { get; set; }
    [DisplayName("Tên sản phẩm chính")]

    public int? MaMhchinh { get; set; }
    [DisplayName("Kích cỡ")]

    public string? Kichco { get; set; }
    [DisplayName("Chất liệu")]

    public string? Chatlieu { get; set; }
    [DisplayName("Màu sắc")]

    public string? Mausac { get; set; }
    [DisplayName("Đơn vị tính")]

    public string? Donvitinh { get; set; }
    [DisplayName("Ngày tạo")]

    public DateTime? Ngaytao { get; set; }
    [DisplayName("Ngày cập nhật")]

    public DateTime? Ngaycapnhat { get; set; }
    [DisplayName("Trạng thái")]

    public bool? Trangthai { get; set; }
    [DisplayName("Khuyến mãi")]

    public bool? Iskhuyemmai { get; set; }
    [DisplayName("Thương hiệu")]

    public string? Thuonghieu { get; set; }
    [DisplayName("Chi tiết khuyễn mãi Sản phẩm")]
    public virtual ICollection<CtKhuyenMaiSanPham>? CtKhuyenMaiSanPhams { get; set; } = new List<CtKhuyenMaiSanPham>();
    [DisplayName("Chi tiết hoa đơn")]

    public virtual ICollection<Cthoadon>? Cthoadons { get; set; } = new List<Cthoadon>();
    [DisplayName("Danh mục")]
    public virtual Danhmuc? Danhmucs { get; set; } = null!;
}
