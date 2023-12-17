using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Thuonghieusothich
{
    [DisplayName("Mã THST")]

    public int Id { get; set; }
    [DisplayName("Mã khách hàng")]

    public int? Makh { get; set; }
    [DisplayName("Tên thương hiệu yêu thích")]

    public string? Thuonghieuyeuthich { get; set; }
    [DisplayName("Khách hàng")]

    public virtual Khachhang? MakhNavigation { get; set; }
}
