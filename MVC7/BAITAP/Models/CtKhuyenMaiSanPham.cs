using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Models;

[Table("CT_KhuyenMai_SanPham")]
public partial class CtKhuyenMaiSanPham
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [Column("MaCTKM")]
    public int? MaCtkm { get; set; }

    [Column("mamh")]
    public int? Mamh { get; set; }

    [Column("phantramkhuyenmai")]
    public double Phantramkhuyenmai { get; set; }

    [ForeignKey("MaCtkm")]
    [InverseProperty("CtKhuyenMaiSanPhams")]
    public virtual CtKhuyenMai? MaCtkmNavigation { get; set; }

    [ForeignKey("Mamh")]
    [InverseProperty("CtKhuyenMaiSanPhams")]
    public virtual Mathang? MamhNavigation { get; set; }
}
