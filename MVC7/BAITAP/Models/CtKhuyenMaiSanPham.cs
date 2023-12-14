using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class CtKhuyenMaiSanPham
{
    public int Id { get; set; }

    public int? MaCtkm { get; set; }

    public int? Mamh { get; set; }

    public double Phantramkhuyenmai { get; set; }

    public virtual CtKhuyenMai? MaCtkmNavigation { get; set; }

    public virtual Mathang? MamhNavigation { get; set; }
}
