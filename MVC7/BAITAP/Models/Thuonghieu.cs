using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Thuonghieu
{
    [DisplayName("Mã thương hiệu")]

    public int MaThuonghieu { get; set; }
    [DisplayName("Tên thương hiệu")]

    public string TenThuonghieu { get; set; } = null!;
}
