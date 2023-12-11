using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("MATHANG")]
public partial class Mathang
{
    [Key]
    [Column("MaMH")]
    public int MaMh { get; set; }

    [StringLength(100)]
    public string Ten { get; set; } = null!;

    public int GiaGoc { get; set; }

    public int GiaBan { get; set; }

    public int SoLuong { get; set; }

    [StringLength(1000)]
    public string? MoTa { get; set; }

    [StringLength(255)]
    public string? HinhAnh { get; set; }

    [Column("MaDM")]
    public int MaDm { get; set; }

    public int? LuotXem { get; set; }

    public int? LuotMua { get; set; }

    [Column("MaMHCHINH")]
    public int? MaMhchinh { get; set; }

    [StringLength(20)]
    public string? Kichco { get; set; }

    [StringLength(50)]
    public string? Chatlieu { get; set; }

    [StringLength(50)]
    public string? Mausac { get; set; }

    [StringLength(20)]
    public string? Donvitinh { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Ngaytao { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Ngaycapnhat { get; set; }

    [Column("trangthai")]
    public bool? Trangthai { get; set; }

    public bool? Iskhuyemmai { get; set; }

    [StringLength(100)]
    public string? Thuonghieu { get; set; }

    [InverseProperty("MamhNavigation")]
    public virtual ICollection<CtKhuyenMaiSanPham> CtKhuyenMaiSanPhams { get; set; } = new List<CtKhuyenMaiSanPham>();

    [InverseProperty("MamhNavigation")]
    public virtual ICollection<Cthoadon> Cthoadons { get; set; } = new List<Cthoadon>();

    [ForeignKey("MaDm")]
    [InverseProperty("Mathangs")]
    public virtual Danhmuc Danhmucs { get; set; } = null!;
}
