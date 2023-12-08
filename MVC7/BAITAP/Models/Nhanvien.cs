using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class Nhanvien
{
    public int Manv { get; set; }

    public string Ten { get; set; } = null!;

    public int Macv { get; set; }

    public string? Dienthoai { get; set; }

    public string? Email { get; set; }

    public string? Matkhau { get; set; }

    public virtual Chucvu? MacvNavigation { get; set; } = null!;
}
