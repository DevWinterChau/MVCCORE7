using System;
using System.Collections.Generic;
using BAITAP.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BAITAP.Data;

public partial class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext()
    {
    }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

   

    public virtual DbSet<Chucvu> Chucvus { get; set; }

    public virtual DbSet<CtKhuyenMai> CtKhuyenMais { get; set; }

    public virtual DbSet<CtKhuyenMaiSanPham> CtKhuyenMaiSanPhams { get; set; }

    public virtual DbSet<Cthoadon> Cthoadons { get; set; }

    public virtual DbSet<Danhmuc> Danhmucs { get; set; }

    public virtual DbSet<Diachi> Diachis { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<LoaiKhuyenMai> LoaiKhuyenMais { get; set; }

    public virtual DbSet<Mathang> Mathangs { get; set; }

    public virtual DbSet<Mh> Mhs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Thuonghieu> Thuonghieus { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       
        modelBuilder.Entity<Chucvu>(entity =>
        {
            entity.HasKey(e => e.Macv).HasName("PK__CHUCVU__603F1834010604A8");
        });

        modelBuilder.Entity<CtKhuyenMai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CT_Khuye__3214EC2741FF704A");

            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.MaLoaiKmNavigation).WithMany(p => p.CtKhuyenMais).HasConstraintName("FK_KhuyenMai_LoaiKhuyenMai");

            entity.HasOne(d => d.NhomSpkhuyemaiNavigation).WithMany(p => p.CtKhuyenMais).HasConstraintName("FK_KhuyenMai_NhomSP");
        });

        modelBuilder.Entity<CtKhuyenMaiSanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CT_Khuye__3214EC2730B44595");

            entity.HasOne(d => d.MaCtkmNavigation).WithMany(p => p.CtKhuyenMaiSanPhams).HasConstraintName("FK_CTKM_KhuyenMai");

            entity.HasOne(d => d.MamhNavigation).WithMany(p => p.CtKhuyenMaiSanPhams).HasConstraintName("FK_CTKM_SanPham");
        });

        modelBuilder.Entity<Cthoadon>(entity =>
        {
            entity.HasKey(e => e.Macthd).HasName("PK__CTHOADON__F50CB4CBA9D41E7D");

            entity.HasOne(d => d.MahdNavigation).WithMany(p => p.Cthoadons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTHOADON__MAHD__5EBF139D");

            entity.HasOne(d => d.MamhNavigation).WithMany(p => p.Cthoadons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTHOADON__MAMH__5FB337D6");
        });

        modelBuilder.Entity<Danhmuc>(entity =>
        {
            entity.HasKey(e => e.MaDm).HasName("PK__DANHMUC__2725866E73F1DDB5");
        });

        modelBuilder.Entity<Diachi>(entity =>
        {
            entity.HasKey(e => e.MaDc).HasName("PK__DIACHI__27258664151C1A0F");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Diachis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIACHI__MAKH__60A75C0F");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.Mahd).HasName("PK__HOADON__603F20CE92CB94EE");

            entity.Property(e => e.Ngay).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Hoadons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOADON__MAKH__619B8048");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KHACHHAN__2725CF1ECA0A2269");
        });

        modelBuilder.Entity<LoaiKhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaLoaiKm).HasName("PK__LoaiKhuy__12250B73F34CED50");

            entity.Property(e => e.MaLoaiKm).ValueGeneratedNever();
        });

        modelBuilder.Entity<Mathang>(entity =>
        {
            entity.HasKey(e => e.MaMh).HasName("PK__MATHANG__2725DFD9C9119724");

            entity.Property(e => e.GiaBan).HasDefaultValueSql("((0))");
            entity.Property(e => e.GiaGoc).HasDefaultValueSql("((0))");
            entity.Property(e => e.LuotMua).HasDefaultValueSql("((0))");
            entity.Property(e => e.LuotXem).HasDefaultValueSql("((0))");

            entity.HasOne(d => d.Danhmucs).WithMany(p => p.Mathangs)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MATHANG__MaDM__628FA481");
        });

        modelBuilder.Entity<Mh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MH__3214EC2719CF2D78");

            entity.Property(e => e.Hinhanh).HasDefaultValueSql("('Chua có ?nh')");
            entity.Property(e => e.Ngaycapnhat).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Ngaytao).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Danhmucs).WithMany(p => p.Mhs).HasConstraintName("FK_MH_DANHMUC");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Manv).HasName("PK__NHANVIEN__603F51149C0A602D");

            entity.HasOne(d => d.MacvNavigation).WithMany(p => p.Nhanviens)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NHANVIEN__MACV__6383C8BA");
        });
        base.OnModelCreating(modelBuilder);
       // OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
