using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class Cthoadon
{
    public int Macthd { get; set; }

    public int Mahd { get; set; }

    public int Mamh { get; set; }

    public int? Dongia { get; set; }

    public int Soluong { get; set; }

    public int? Thanhtien { get; set; }

    public virtual Hoadon MahdNavigation { get; set; } = null!;

    public virtual Mathang MamhNavigation { get; set; } = null!;
}
