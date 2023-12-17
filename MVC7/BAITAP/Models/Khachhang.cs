using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Khachhang
{
    [DisplayName("Mã khách hàng")]
    public int MaKh { get; set; }
    [DisplayName("Họ tên")]

    public string? Ten { get; set; }
    [DisplayName("Điện thoại")]

    public string Dienthoai { get; set; } = null!;
    [DisplayName("Email")]

    public string Email { get; set; } = null!;
    [DisplayName("Mật khẩu")]

    public string? Matkhau { get; set; }

    public byte[] Salt { get; set; } = null!;
    [DisplayName("Chi tiết danh mục yêu thích")]

    public virtual ICollection<Danhmucsothich> Danhmucsothiches { get; set; } = new List<Danhmucsothich>();
    [DisplayName("Danh sách địa chỉ")]

    public virtual ICollection<Diachi> Diachis { get; set; } = new List<Diachi>();
    [DisplayName("Danh sách hóa đơn")]

    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();
    [DisplayName("Chi tiết thương hiệu yêu thích")]

    public virtual ICollection<Thuonghieusothich> Thuonghieusothiches { get; set; } = new List<Thuonghieusothich>();
}
