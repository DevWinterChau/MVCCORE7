using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class LoaiKhuyenMai
{
    [DisplayName("Mã loại khuyến mãi")]

    public int MaLoaiKm { get; set; }
    [DisplayName("Tên loại khuyến mãi")]

    public string TenLoaiKm { get; set; } = null!;
    [DisplayName("Chương trình khuyến mãi")]

    public virtual ICollection<CtKhuyenMai> CtKhuyenMais { get; set; } = new List<CtKhuyenMai>();
}
