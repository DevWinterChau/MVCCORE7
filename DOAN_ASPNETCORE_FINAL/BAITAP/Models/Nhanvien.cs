using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Nhanvien
{
    [DisplayName("Mã nhân viên")]

    public int Manv { get; set; }
    [DisplayName("tên nhân viên")]

    public string Ten { get; set; } = null!;
    [DisplayName("Mã chức vụ")]

    public int Macv { get; set; }
    [DisplayName("Điện thoại")]

    public string? Dienthoai { get; set; }
    [DisplayName("Email")]

    public string? Email { get; set; }
    [DisplayName("Mã tài khoản đăng nhập")]
    public string? UserID { get; set; }

    [DisplayName("Chức vụ")]

    public virtual Chucvu MacvNavigation { get; set; } = null!;
}
