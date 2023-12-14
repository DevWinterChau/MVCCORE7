using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class Hoadon
{
    public int Mahd { get; set; }

    public int Makh { get; set; }

    public DateTime? Ngay { get; set; }

    public int Tongtien { get; set; }

    public int Trangthai { get; set; }

    public string? Tennguoinhan { get; set; }

    public string? Sodienthoai { get; set; }

    public string? Diachi { get; set; }

    public string? Xaphuong { get; set; }

    public string? Quanhuyen { get; set; }

    public string? Tinh { get; set; }

    public virtual ICollection<Cthoadon> Cthoadons { get; set; } = new List<Cthoadon>();

    public virtual Khachhang MakhNavigation { get; set; } = null!;
}
