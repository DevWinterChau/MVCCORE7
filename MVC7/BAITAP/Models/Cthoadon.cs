using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("CTHOADON")]
public partial class Cthoadon
{
    [Key]
    [Column("MACTHD")]
    public int Macthd { get; set; }

    [Column("MAHD")]
    public int Mahd { get; set; }

    [Column("MAMH")]
    public int Mamh { get; set; }

    [Column("DONGIA")]
    public int? Dongia { get; set; }

    [Column("SOLUONG")]
    public int Soluong { get; set; }

    [Column("THANHTIEN")]
    public int? Thanhtien { get; set; }

    [ForeignKey("Mahd")]
    [InverseProperty("Cthoadons")]
    public virtual Hoadon MahdNavigation { get; set; } = null!;

    [ForeignKey("Mamh")]
    [InverseProperty("Cthoadons")]
    public virtual Mathang MamhNavigation { get; set; } = null!;
}
