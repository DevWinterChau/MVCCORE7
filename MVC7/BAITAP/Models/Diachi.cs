using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class Diachi
{
    public int MaDc { get; set; }

    public int Makh { get; set; }

    public string Diachi1 { get; set; } = null!;

    public string Phuongxa { get; set; } = null!;

    public string Quanhuyen { get; set; } = null!;

    public string Tinhthanh { get; set; } = null!;

    public int Macdinh { get; set; }

    public virtual Khachhang MakhNavigation { get; set; } = null!;
}
