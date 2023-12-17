using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace BAITAP.Models;

public partial class Mh
{
    [DisplayName("Mã sản phẩm chính")]

    public int Id { get; set; }
    [DisplayName("Tên sản phẩm chính")]
    public string Ten { get; set; } = null!;
    [DisplayName("Hình ảnh")]

    public string? Hinhanh { get; set; }
    [DisplayName("Ngày tạo")]

    public DateTime Ngaytao { get; set; }
    [DisplayName("Ngày cập nhật")]

    public DateTime Ngaycapnhat { get; set; }
    [DisplayName("Lượt khách xem")]
    public int? LuotXem { get; set; }
    [DisplayName("Mã danh mục")]

    public int? Madm { get; set; }
    [DisplayName("Mô tả")]
    public string? Mota { get; set; }
    [DisplayName("Danh mục")]

    public virtual Danhmuc? Danhmucs { get; set; }
}
