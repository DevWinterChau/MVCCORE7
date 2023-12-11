using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("CHUCVU")]
public partial class Chucvu
{
    [Key]
    [Column("MACV")]
    public int Macv { get; set; }

    [Column("TEN")]
    [StringLength(100)]
    public string Ten { get; set; } = null!;

    [Column("HESO")]
    public double? Heso { get; set; }

    [InverseProperty("MacvNavigation")]
    public virtual ICollection<Nhanvien> Nhanviens { get; set; } = new List<Nhanvien>();
}
