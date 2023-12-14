using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class Mh
{
    public int Id { get; set; }

    public string Ten { get; set; } = null!;

    public string? Hinhanh { get; set; }

    public DateTime Ngaytao { get; set; }

    public DateTime Ngaycapnhat { get; set; }
    public int? LuotXem { get; set; }

    public int? Madm { get; set; }
    public string? Mota { get; set; }    

    public virtual Danhmuc? Danhmucs { get; set; }
}
