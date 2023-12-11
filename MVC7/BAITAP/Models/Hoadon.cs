using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("HOADON")]
public partial class Hoadon
{
    [Key]
    [Column("MAHD")]
    public int Mahd { get; set; }

    [Column("MAKH")]
    public int Makh { get; set; }

    [Column("NGAY", TypeName = "datetime")]
    public DateTime? Ngay { get; set; }

    [Column("TONGTIEN")]
    public int Tongtien { get; set; }

    [Column("TRANGTHAI")]
    public int Trangthai { get; set; }

    [StringLength(20)]
    public string? Tennguoinhan { get; set; }

    [StringLength(20)]
    public string? Sodienthoai { get; set; }

    [StringLength(80)]
    public string? Diachi { get; set; }

    [StringLength(30)]
    public string? Xaphuong { get; set; }

    [StringLength(30)]
    public string? Quanhuyen { get; set; }

    [StringLength(30)]
    public string? Tinh { get; set; }

    [InverseProperty("MahdNavigation")]
    public virtual ICollection<Cthoadon> Cthoadons { get; set; } = new List<Cthoadon>();

    [ForeignKey("Makh")]
    [InverseProperty("Hoadons")]
    public virtual Khachhang MakhNavigation { get; set; } = null!;
}
