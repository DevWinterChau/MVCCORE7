using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("LoaiKhuyenMai")]
public partial class LoaiKhuyenMai
{
    [Key]
    [Column("MaLoaiKM")]
    public int MaLoaiKm { get; set; }

    [Column("TenLoaiKM")]
    [StringLength(255)]
    [Unicode(false)]
    public string TenLoaiKm { get; set; } = null!;

    [InverseProperty("MaLoaiKmNavigation")]
    public virtual ICollection<CtKhuyenMai> CtKhuyenMais { get; set; } = new List<CtKhuyenMai>();
}
