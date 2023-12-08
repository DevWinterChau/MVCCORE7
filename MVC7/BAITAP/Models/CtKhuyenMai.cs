using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class CtKhuyenMai
{
    public int Id { get; set; }

    public string? TenKm { get; set; }

    public string? MoTa { get; set; }

    public DateTime NgayBatDau { get; set; }

    public DateTime NgayKetThuc { get; set; }

    public int Soluongmuatoithieu { get; set; }

    public int Sotienmuatoithieu { get; set; }

    public int? PhanTramGiamGia { get; set; }

    public decimal? GiaGiam { get; set; }

    public decimal? DieuKienApDung { get; set; }

    public int Soluongsudung { get; set; }

    public int? NhomSpkhuyemai { get; set; }

    public bool TrangThai { get; set; }

    public int? MaLoaiKm { get; set; }

    public virtual ICollection<CtKhuyenMaiSanPham> CtKhuyenMaiSanPhams { get; set; } = new List<CtKhuyenMaiSanPham>();

    public virtual LoaiKhuyenMai? MaLoaiKmNavigation { get; set; }

    public virtual Danhmuc? NhomSpkhuyemaiNavigation { get; set; }
}
