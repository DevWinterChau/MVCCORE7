using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("DIACHI")]
public partial class Diachi
{
    [Key]
    [Column("MaDC")]
    public int MaDc { get; set; }

    [Column("MAKH")]
    public int Makh { get; set; }

    [Column("DIACHI")]
    [StringLength(100)]
    public string Diachi1 { get; set; } = null!;

    [Column("PHUONGXA")]
    [StringLength(20)]
    public string Phuongxa { get; set; } = null!;

    [Column("QUANHUYEN")]
    [StringLength(50)]
    public string Quanhuyen { get; set; } = null!;

    [Column("TINHTHANH")]
    [StringLength(50)]
    public string Tinhthanh { get; set; } = null!;

    [Column("MACDINH")]
    public int Macdinh { get; set; }

    [ForeignKey("Makh")]
    [InverseProperty("Diachis")]
    public virtual Khachhang MakhNavigation { get; set; } = null!;
}
