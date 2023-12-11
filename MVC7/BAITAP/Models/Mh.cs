using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("MH")]
public partial class Mh
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("TEN")]
    [StringLength(100)]
    public string Ten { get; set; } = null!;

    [Column("HINHANH")]
    [StringLength(255)]
    public string? Hinhanh { get; set; }

    [Column("NGAYTAO", TypeName = "datetime")]
    public DateTime Ngaytao { get; set; }

    [Column("NGAYCAPNHAT", TypeName = "datetime")]
    public DateTime Ngaycapnhat { get; set; }

    [Column("MADM")]
    public int? Madm { get; set; }

    [ForeignKey("Madm")]
    [InverseProperty("Mhs")]
    public virtual Danhmuc? Danhmucs { get; set; }
}
