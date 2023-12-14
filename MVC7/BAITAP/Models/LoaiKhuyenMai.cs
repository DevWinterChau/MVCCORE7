using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class LoaiKhuyenMai
{
    public int MaLoaiKm { get; set; }

    public string TenLoaiKm { get; set; } = null!;

    public virtual ICollection<CtKhuyenMai> CtKhuyenMais { get; set; } = new List<CtKhuyenMai>();
}
