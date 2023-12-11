using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("DANHMUC")]
public partial class Danhmuc
{
    [Key]
    [Column("MaDM")]
    public int MaDm { get; set; }

    [StringLength(100)]
    public string Ten { get; set; } = null!;

    [InverseProperty("NhomSpkhuyemaiNavigation")]
    public virtual ICollection<CtKhuyenMai> CtKhuyenMais { get; set; } = new List<CtKhuyenMai>();

    [InverseProperty("MaDmNavigation")]
    public virtual ICollection<Mathang> Mathangs { get; set; } = new List<Mathang>();

    [InverseProperty("MadmNavigation")]
    public virtual ICollection<Mh> Mhs { get; set; } = new List<Mh>();
}
