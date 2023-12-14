using System;
using System.Collections.Generic;

namespace BAITAP.Models;

public partial class Thuonghieusothich
{
    public int Id { get; set; }

    public int? Makh { get; set; }

    public string? Thuonghieuyeuthich { get; set; }

    public virtual Khachhang? MakhNavigation { get; set; }
}
