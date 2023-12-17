using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Danhmucsothich
{
    [DisplayName("Mã")]
    public int Id { get; set; }
    [DisplayName("Mã khách hàng")]
    public int? Makh { get; set; }
    [DisplayName("Tên danh mục yêu thích")]
    public string? Loaisanphamyeuthich { get; set; }
    [DisplayName("Khách hàng")]

    public virtual Khachhang? MakhNavigation { get; set; }
}
