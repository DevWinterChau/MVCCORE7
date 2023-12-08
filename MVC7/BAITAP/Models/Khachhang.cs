using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class Khachhang
{
    public int MaKh { get; set; }

    public string? Ten { get; set; }

    public string Dienthoai { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Matkhau { get; set; } = null!;

    public virtual ICollection<Diachi> Diachis { get; set; } = new List<Diachi>();

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();
}
