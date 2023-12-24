using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class CtKhuyenMai
{
    [DisplayName("Mã khuyến mãi")]
    public int Id { get; set; }
    [DisplayName("Tên khuyến mãi")]

    public string? TenKm { get; set; }
    [DisplayName("Mô tả")]

    public string? MoTa { get; set; }
    [DisplayName("Ngày bắt đầu")]

    public DateTime NgayBatDau { get; set; }
    [DisplayName("Ngày kết thúc")]

    public DateTime NgayKetThuc { get; set; }
    [DisplayName("Số lượng mua tối thiểu")]

    public int Soluongmuatoithieu { get; set; }
    [DisplayName("Số tiền mua tối thiểu")]

    public int Sotienmuatoithieu { get; set; }
    [DisplayName("Phần trắm giảm giá")]

    public int PhanTramGiamGia { get; set; }
    [DisplayName("Giá được giảm")]

    public decimal? GiaGiam { get; set; }
    [DisplayName("Điều kiện áp dụng")]
    public decimal? DieuKienApDung { get; set; }
    [DisplayName("Số lượng sử dụng")]

    public int Soluongsudung { get; set; }
    [DisplayName("Nhóm sản phẩm áp dụng")]

    public int? NhomSpkhuyemai { get; set; }
    [DisplayName("Trạng thái")]

    public bool TrangThai { get; set; }
    [DisplayName("Loại khuyến mãi")]
    public int? MaLoaiKm { get; set; }
    [DisplayName("Chi tiết khuyến mãi")]

    public virtual ICollection<CtKhuyenMaiSanPham> CtKhuyenMaiSanPhams { get; set; } = new List<CtKhuyenMaiSanPham>();
    [DisplayName("Loại khuyến mãi")]

    public virtual LoaiKhuyenMai? MaLoaiKmNavigation { get; set; }
    [DisplayName("Danh mục")]
    public virtual Danhmuc? NhomSpkhuyemaiNavigation { get; set; }
}
