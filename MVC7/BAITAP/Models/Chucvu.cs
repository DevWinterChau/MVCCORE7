using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Chucvu
{
    [DisplayName("Mã chức vụ")]

    public int Macv { get; set; }
    [DisplayName("Tên chức vụ")]

    public string Ten { get; set; } = null!;
    [DisplayName("Hệ số lương")]

    public double? Heso { get; set; }

    public virtual ICollection<Nhanvien> Nhanviens { get; set; } = new List<Nhanvien>();
}
