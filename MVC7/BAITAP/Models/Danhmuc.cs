using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class Danhmuc
{
    public int MaDm { get; set; }

    public string Ten { get; set; } = null!;

    public virtual ICollection<CtKhuyenMai> CtKhuyenMais { get; set; } = new List<CtKhuyenMai>();

    public virtual ICollection<Mathang> Mathangs { get; set; } = new List<Mathang>();

    public virtual ICollection<Mh> Mhs { get; set; } = new List<Mh>();
}
