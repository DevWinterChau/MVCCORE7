using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class Danhmucsothich
{
    public int Id { get; set; }

    public int? Makh { get; set; }

    public string? Loaisanphamyeuthich { get; set; }

    public virtual Khachhang? MakhNavigation { get; set; }
}
