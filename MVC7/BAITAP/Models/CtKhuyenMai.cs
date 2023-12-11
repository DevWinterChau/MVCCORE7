using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("CT_KhuyenMai")]
public partial class CtKhuyenMai
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("TenKM")]
    [StringLength(100)]
    public string? TenKm { get; set; }

    [StringLength(50)]
    public string? MoTa { get; set; }

    [Column(TypeName = "date")]
    public DateTime NgayBatDau { get; set; }

    [Column(TypeName = "date")]
    public DateTime NgayKetThuc { get; set; }

    public int Soluongmuatoithieu { get; set; }

    public int Sotienmuatoithieu { get; set; }

    public int PhanTramGiamGia { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? GiaGiam { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? DieuKienApDung { get; set; }

    public int Soluongsudung { get; set; }

    [Column("NhomSPKhuyemai")]
    public int? NhomSpkhuyemai { get; set; }

    public bool TrangThai { get; set; }

    [Column("MaLoaiKM")]
    public int? MaLoaiKm { get; set; }

    [InverseProperty("MaCtkmNavigation")]
    public virtual ICollection<CtKhuyenMaiSanPham> CtKhuyenMaiSanPhams { get; set; } = new List<CtKhuyenMaiSanPham>();

    [ForeignKey("MaLoaiKm")]
    [InverseProperty("CtKhuyenMais")]
    public virtual LoaiKhuyenMai? MaLoaiKmNavigation { get; set; }

    [ForeignKey("NhomSpkhuyemai")]
    [InverseProperty("CtKhuyenMais")]
    public virtual Danhmuc? NhomSpkhuyemaiNavigation { get; set; }
}
