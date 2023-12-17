using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Hoadon
{
    [DisplayName("Mã hóa đơn")]

    public int Mahd { get; set; }
    [DisplayName("Mã khách hàng")]

    public int Makh { get; set; }
    [DisplayName("Ngày tạo")]

    public DateTime? Ngay { get; set; }
    [DisplayName("Tổng tiền")]

    public int Tongtien { get; set; }
    [DisplayName("Trạng thái")]

    public int Trangthai { get; set; }
    [DisplayName("Tên người nhận")]

    public string? Tennguoinhan { get; set; }
    [DisplayName("Số điện thoại")]

    public string? Sodienthoai { get; set; }
    [DisplayName("Địa chỉ nhận")]

    public string? Diachi { get; set; }
    [DisplayName("Phường xã")]

    public string? Xaphuong { get; set; }
    [DisplayName("Quận huyện")]

    public string? Quanhuyen { get; set; }
    [DisplayName("Tỉnh")]

    public string? Tinh { get; set; }
    [DisplayName("Chi tiết hóa đơn")]

    public virtual ICollection<Cthoadon>? Cthoadons { get; set; } = new List<Cthoadon>();
    [DisplayName("Khách hàng")]
    public virtual Khachhang? MakhNavigation { get; set; } = null!;
}
