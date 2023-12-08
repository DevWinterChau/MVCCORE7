using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class Mathang
{
    public int MaMh { get; set; }

    public string Ten { get; set; } = null!;

    public int GiaGoc { get; set; }

    public int GiaBan { get; set; }

    public int SoLuong { get; set; }

    public string? MoTa { get; set; }

    public string? HinhAnh { get; set; }

    public int MaDm { get; set; }

    public int? LuotXem { get; set; }

    public int? LuotMua { get; set; }

    public int? MaMhchinh { get; set; }

    public string? Kichco { get; set; }

    public string? Chatlieu { get; set; }

    public string? Mausac { get; set; }

    public string? Donvitinh { get; set; }

    public DateTime? Ngaytao { get; set; }

    public DateTime? Ngaycapnhat { get; set; }

    public bool? Trangthai { get; set; }

    public bool? Iskhuyemmai { get; set; }

    public string? Thuonghieu { get; set; }

    public virtual ICollection<CtKhuyenMaiSanPham>? CtKhuyenMaiSanPhams { get; set; } = new List<CtKhuyenMaiSanPham>();

    public virtual ICollection<Cthoadon>? Cthoadons { get; set; } = new List<Cthoadon>();

    public virtual Danhmuc? Danhmucs { get; set; } = null!;
}
