using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Danhmuc
{
    [DisplayName("Mã danh mục")]
    public int MaDm { get; set; }
    [DisplayName("Tên danh mục")]

    public string Ten { get; set; } = null!;
    [DisplayName("Chương trình khuyến mãi")]

    public virtual ICollection<CtKhuyenMai> CtKhuyenMais { get; set; } = new List<CtKhuyenMai>();
    [DisplayName("Phiên bản sản phẩm")]

    public virtual ICollection<Mathang> Mathangs { get; set; } = new List<Mathang>();
    [DisplayName("Sản phẩm")]
    public virtual ICollection<Mh> Mhs { get; set; } = new List<Mh>();
}
