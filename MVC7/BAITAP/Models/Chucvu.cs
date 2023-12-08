using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class Chucvu
{
    public int Macv { get; set; }

    public string Ten { get; set; } = null!;

    public double? Heso { get; set; }

    public virtual ICollection<Nhanvien> Nhanviens { get; set; } = new List<Nhanvien>();
}
