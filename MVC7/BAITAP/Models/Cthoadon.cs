using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Cthoadon
{
    [DisplayName("Mã CTHD")]

    public int Macthd { get; set; }
    [DisplayName("Mã Hóa đơn")]

    public int Mahd { get; set; }
    [DisplayName("Mã Sản Phẩm")]
    public int Mamh { get; set; }
    [DisplayName("Đơn giá bán")]

    public int Dongia { get; set; }
    [DisplayName("Số lượng mua")]

    public int Soluong { get; set; }
    [DisplayName("Thành tiền")]

    public int Thanhtien { get; set; }
    [DisplayName("Hóa đơn")]

    public virtual Hoadon MahdNavigation { get; set; } = null!;
    [DisplayName("Sản phẩm")]

    public virtual Mathang MamhNavigation { get; set; } = null!;
}
