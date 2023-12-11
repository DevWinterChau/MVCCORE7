using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("Thuonghieu")]
public partial class Thuonghieu
{
    [Key]
    public int MaThuonghieu { get; set; }

    [StringLength(100)]
    public string TenThuonghieu { get; set; } = null!;
}
