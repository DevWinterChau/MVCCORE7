using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("KHACHHANG")]
public partial class Khachhang
{
    [Key]
    [Column("MaKH")]
    public int MaKh { get; set; }

    [StringLength(100)]
    public string? Ten { get; set; }

    [Column("DIENTHOAI")]
    [StringLength(20)]
    [Unicode(false)]
    public string Dienthoai { get; set; } = null!;

    [Column("EMAIL")]
    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [Column("MATKHAU")]
    [StringLength(50)]
    [Unicode(false)]
    public string Matkhau { get; set; } = null!;

    [InverseProperty("MakhNavigation")]
    public virtual ICollection<Diachi> Diachis { get; set; } = new List<Diachi>();

    [InverseProperty("MakhNavigation")]
    public virtual ICollection<Hoadon> Hoadons { get; set; } = new List<Hoadon>();
}
