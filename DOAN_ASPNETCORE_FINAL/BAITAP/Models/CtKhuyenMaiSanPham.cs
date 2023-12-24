using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BAITAP.Models;

public partial class CtKhuyenMaiSanPham
{
    [Key]
    [DisplayName("Mã Chi Tiết Khuyến Mãi Sản Phẩm")]
    public int Id { get; set; }
    [DisplayName("Chương Trình Khuyến Mãi")]
    public int? MaCtkm { get; set; }
    [DisplayName("Sản phẩm")]
    public int Mamh { get; set; }
    [DisplayName("Phần trăm khuyến mãi")]
    public double Phantramkhuyenmai { get; set; }
    [DisplayName("Chương trình khuyến mãi")]

    public virtual CtKhuyenMai? MaCtkmNavigation { get; set; }
    [DisplayName("Sản phẩm")]
    public virtual Mathang? MamhNavigation { get; set; }
}
