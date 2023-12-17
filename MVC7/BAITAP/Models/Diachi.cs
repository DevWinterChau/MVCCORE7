using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Diachi
{
    [DisplayName("Mã địa chỉ")]
    public int MaDc { get; set; }
    [DisplayName("Mã khách hàng")]
    public int Makh { get; set; }
    [DisplayName("Địa chỉ chi tiết")]
    public string Diachi1 { get; set; } = null!;
    [DisplayName("Phường xã")]

    public string Phuongxa { get; set; } = null!;
    [DisplayName("Quận huyện")]

    public string Quanhuyen { get; set; } = null!;
    [DisplayName("Tỉnh thành")]

    public string Tinhthanh { get; set; } = null!;
    [DisplayName("Đặt làm mặc định")]

    public int Macdinh { get; set; }
    [DisplayName("Khách hàng")]

    public virtual Khachhang MakhNavigation { get; set; } = null!;
}
