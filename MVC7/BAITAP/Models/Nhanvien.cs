using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("NHANVIEN")]
public partial class Nhanvien
{
    [Key]
    [Column("MANV")]
    public int Manv { get; set; }

    [Column("TEN")]
    [StringLength(100)]
    public string Ten { get; set; } = null!;

    [Column("MACV")]
    public int Macv { get; set; }

    [Column("DIENTHOAI")]
    [StringLength(20)]
    [Unicode(false)]
    public string? Dienthoai { get; set; }

    [Column("EMAIL")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Email { get; set; }

    [Column("MATKHAU")]
    [StringLength(50)]
    [Unicode(false)]
    public string? Matkhau { get; set; }

    [ForeignKey("Macv")]
    [InverseProperty("Nhanviens")]
    public virtual Chucvu MacvNavigation { get; set; } = null!;
}
