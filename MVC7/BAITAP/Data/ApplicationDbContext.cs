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

    public virtual DbSet<Danhmucsothich> Danhmucsothiches { get; set; }

    public virtual DbSet<Diachi> Diachis { get; set; }

    public virtual DbSet<Hoadon> Hoadons { get; set; }

    public virtual DbSet<Khachhang> Khachhangs { get; set; }

    public virtual DbSet<LoaiKhuyenMai> LoaiKhuyenMais { get; set; }

    public virtual DbSet<Mathang> Mathangs { get; set; }

    public virtual DbSet<Mh> Mhs { get; set; }

    public virtual DbSet<Nhanvien> Nhanviens { get; set; }

    public virtual DbSet<Thuonghieu> Thuonghieus { get; set; }

    public virtual DbSet<Thuonghieusothich> Thuonghieusothiches { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       

        modelBuilder.Entity<Chucvu>(entity =>
        {
            entity.HasKey(e => e.Macv).HasName("PK__CHUCVU__603F1834010604A8");

            entity.ToTable("CHUCVU");

            entity.Property(e => e.Macv).HasColumnName("MACV");
            entity.Property(e => e.Heso).HasColumnName("HESO");
            entity.Property(e => e.Ten)
                .HasMaxLength(100)
                .HasColumnName("TEN");
        });

        modelBuilder.Entity<CtKhuyenMai>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CT_Khuye__3214EC2741FF704A");

            entity.ToTable("CT_KhuyenMai");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("ID");
            entity.Property(e => e.DieuKienApDung).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.GiaGiam).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MaLoaiKm).HasColumnName("MaLoaiKM");
            entity.Property(e => e.MoTa).HasMaxLength(50);
            entity.Property(e => e.NgayBatDau).HasColumnType("date");
            entity.Property(e => e.NgayKetThuc).HasColumnType("date");
            entity.Property(e => e.NhomSpkhuyemai).HasColumnName("NhomSPKhuyemai");
            entity.Property(e => e.TenKm)
                .HasMaxLength(100)
                .HasColumnName("TenKM");

            entity.HasOne(d => d.MaLoaiKmNavigation).WithMany(p => p.CtKhuyenMais)
                .HasForeignKey(d => d.MaLoaiKm)
                .HasConstraintName("FK_KhuyenMai_LoaiKhuyenMai");

            entity.HasOne(d => d.NhomSpkhuyemaiNavigation).WithMany(p => p.CtKhuyenMais)
                .HasForeignKey(d => d.NhomSpkhuyemai)
                .HasConstraintName("FK_KhuyenMai_NhomSP");
        });

        modelBuilder.Entity<CtKhuyenMaiSanPham>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__CT_Khuye__3214EC2730B44595");

            entity.ToTable("CT_KhuyenMai_SanPham");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.MaCtkm).HasColumnName("MaCTKM");
            entity.Property(e => e.Mamh).HasColumnName("mamh");
            entity.Property(e => e.Phantramkhuyenmai).HasColumnName("phantramkhuyenmai");

            entity.HasOne(d => d.MaCtkmNavigation).WithMany(p => p.CtKhuyenMaiSanPhams)
                .HasForeignKey(d => d.MaCtkm)
                .HasConstraintName("FK_CTKM_KhuyenMai");

            entity.HasOne(d => d.MamhNavigation).WithMany(p => p.CtKhuyenMaiSanPhams)
                .HasForeignKey(d => d.Mamh)
                .HasConstraintName("FK_CTKM_SanPham");
        });

        modelBuilder.Entity<Cthoadon>(entity =>
        {
            entity.HasKey(e => e.Macthd).HasName("PK__CTHOADON__F50CB4CBA9D41E7D");

            entity.ToTable("CTHOADON");

            entity.Property(e => e.Macthd).HasColumnName("MACTHD");
            entity.Property(e => e.Dongia).HasColumnName("DONGIA");
            entity.Property(e => e.Mahd).HasColumnName("MAHD");
            entity.Property(e => e.Mamh).HasColumnName("MAMH");
            entity.Property(e => e.Soluong).HasColumnName("SOLUONG");
            entity.Property(e => e.Thanhtien).HasColumnName("THANHTIEN");

            entity.HasOne(d => d.MahdNavigation).WithMany(p => p.Cthoadons)
                .HasForeignKey(d => d.Mahd)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTHOADON__MAHD__5EBF139D");

            entity.HasOne(d => d.MamhNavigation).WithMany(p => p.Cthoadons)
                .HasForeignKey(d => d.Mamh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CTHOADON__MAMH__5FB337D6");
        });

        modelBuilder.Entity<Danhmuc>(entity =>
        {
            entity.HasKey(e => e.MaDm).HasName("PK__DANHMUC__2725866E73F1DDB5");

            entity.ToTable("DANHMUC");

            entity.Property(e => e.MaDm).HasColumnName("MaDM");
            entity.Property(e => e.Ten).HasMaxLength(100);
        });

        modelBuilder.Entity<Danhmucsothich>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__DANHMUCS__3214EC27A78BD6E1");

            entity.ToTable("DANHMUCSOTHICH");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Loaisanphamyeuthich)
                .HasMaxLength(255)
                .HasColumnName("LOAISANPHAMYEUTHICH");
            entity.Property(e => e.Makh).HasColumnName("MAKH");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Danhmucsothiches)
                .HasForeignKey(d => d.Makh)
                .HasConstraintName("FK_DMST_KH");
        });

        modelBuilder.Entity<Diachi>(entity =>
        {
            entity.HasKey(e => e.MaDc).HasName("PK__DIACHI__27258664151C1A0F");

            entity.ToTable("DIACHI");

            entity.Property(e => e.MaDc).HasColumnName("MaDC");
            entity.Property(e => e.Diachi1)
                .HasMaxLength(100)
                .HasColumnName("DIACHI");
            entity.Property(e => e.Macdinh).HasColumnName("MACDINH");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Phuongxa)
                .HasMaxLength(20)
                .HasColumnName("PHUONGXA");
            entity.Property(e => e.Quanhuyen)
                .HasMaxLength(50)
                .HasColumnName("QUANHUYEN");
            entity.Property(e => e.Tinhthanh)
                .HasMaxLength(50)
                .HasColumnName("TINHTHANH");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Diachis)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DIACHI__MAKH__60A75C0F");
        });

        modelBuilder.Entity<Hoadon>(entity =>
        {
            entity.HasKey(e => e.Mahd).HasName("PK__HOADON__603F20CE92CB94EE");

            entity.ToTable("HOADON");

            entity.Property(e => e.Mahd).HasColumnName("MAHD");
            entity.Property(e => e.Diachi).HasMaxLength(80);
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Ngay)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAY");
            entity.Property(e => e.Quanhuyen).HasMaxLength(30);
            entity.Property(e => e.Sodienthoai).HasMaxLength(20);
            entity.Property(e => e.Tennguoinhan).HasMaxLength(20);
            entity.Property(e => e.Tinh).HasMaxLength(30);
            entity.Property(e => e.Tongtien).HasColumnName("TONGTIEN");
            entity.Property(e => e.Trangthai).HasColumnName("TRANGTHAI");
            entity.Property(e => e.Xaphuong).HasMaxLength(30);

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Hoadons)
                .HasForeignKey(d => d.Makh)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HOADON__MAKH__619B8048");
        });

        modelBuilder.Entity<Khachhang>(entity =>
        {
            entity.HasKey(e => e.MaKh).HasName("PK__KHACHHAN__2725CF1ECA0A2269");

            entity.ToTable("KHACHHANG");

            entity.Property(e => e.MaKh).HasColumnName("MaKH");
            entity.Property(e => e.Dienthoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DIENTHOAI");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("MATKHAU");
            entity.Property(e => e.Salt).HasMaxLength(64);
            entity.Property(e => e.Ten).HasMaxLength(100);
        });

        modelBuilder.Entity<LoaiKhuyenMai>(entity =>
        {
            entity.HasKey(e => e.MaLoaiKm).HasName("PK__LoaiKhuy__12250B73F34CED50");

            entity.ToTable("LoaiKhuyenMai");

            entity.Property(e => e.MaLoaiKm)
                .ValueGeneratedNever()
                .HasColumnName("MaLoaiKM");
            entity.Property(e => e.TenLoaiKm)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("TenLoaiKM");
        });

        modelBuilder.Entity<Mathang>(entity =>
        {
            entity.HasKey(e => e.MaMh).HasName("PK__MATHANG__2725DFD9C9119724");

            entity.ToTable("MATHANG");

            entity.Property(e => e.MaMh).HasColumnName("MaMH");
            entity.Property(e => e.Chatlieu).HasMaxLength(50);
            entity.Property(e => e.Donvitinh).HasMaxLength(20);
            entity.Property(e => e.GiaBan).HasDefaultValueSql("((0))");
            entity.Property(e => e.GiaGoc).HasDefaultValueSql("((0))");
            entity.Property(e => e.HinhAnh).HasMaxLength(255);
            entity.Property(e => e.Kichco).HasMaxLength(20);
            entity.Property(e => e.LuotMua).HasDefaultValueSql("((0))");
            entity.Property(e => e.LuotXem).HasDefaultValueSql("((0))");
            entity.Property(e => e.MaDm).HasColumnName("MaDM");
            entity.Property(e => e.MaMhchinh).HasColumnName("MaMHCHINH");
            entity.Property(e => e.Mausac).HasMaxLength(50);
            entity.Property(e => e.MoTa).HasMaxLength(1000);
            entity.Property(e => e.Ngaycapnhat).HasColumnType("datetime");
            entity.Property(e => e.Ngaytao).HasColumnType("datetime");
            entity.Property(e => e.Ten).HasMaxLength(100);
            entity.Property(e => e.Thuonghieu).HasMaxLength(100);
            entity.Property(e => e.Trangthai).HasColumnName("trangthai");

            entity.HasOne(d => d.Danhmucs).WithMany(p => p.Mathangs)
                .HasForeignKey(d => d.MaDm)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MATHANG__MaDM__628FA481");
        });

        modelBuilder.Entity<Mh>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MH__3214EC2719CF2D78");

            entity.ToTable("MH");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Hinhanh)
                .HasMaxLength(255)
                .HasDefaultValueSql("('Chua có ?nh')")
                .HasColumnName("HINHANH");
            entity.Property(e => e.Madm).HasColumnName("MADM");
            entity.Property(e => e.Ngaycapnhat)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYCAPNHAT");
            entity.Property(e => e.Ngaytao)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("NGAYTAO");
            entity.Property(e => e.Ten)
                .HasMaxLength(100)
                .HasColumnName("TEN");

            entity.HasOne(d => d.Danhmucs).WithMany(p => p.Mhs)
                .HasForeignKey(d => d.Madm)
                .HasConstraintName("FK_MH_DANHMUC");
        });

        modelBuilder.Entity<Nhanvien>(entity =>
        {
            entity.HasKey(e => e.Manv).HasName("PK__NHANVIEN__603F51149C0A602D");

            entity.ToTable("NHANVIEN");

            entity.Property(e => e.Manv).HasColumnName("MANV");
            entity.Property(e => e.Dienthoai)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("DIENTHOAI");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Macv).HasColumnName("MACV");
            entity.Property(e => e.Matkhau)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MATKHAU");
            entity.Property(e => e.Ten)
                .HasMaxLength(100)
                .HasColumnName("TEN");

            entity.HasOne(d => d.MacvNavigation).WithMany(p => p.Nhanviens)
                .HasForeignKey(d => d.Macv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__NHANVIEN__MACV__6383C8BA");
        });

        modelBuilder.Entity<Thuonghieu>(entity =>
        {
            entity.HasKey(e => e.MaThuonghieu);

            entity.ToTable("Thuonghieu");

            entity.Property(e => e.TenThuonghieu).HasMaxLength(100);
        });

        modelBuilder.Entity<Thuonghieusothich>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__THUONGHI__3214EC278FCD0A96");

            entity.ToTable("THUONGHIEUSOTHICH");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Makh).HasColumnName("MAKH");
            entity.Property(e => e.Thuonghieuyeuthich)
                .HasMaxLength(255)
                .HasColumnName("THUONGHIEUYEUTHICH");

            entity.HasOne(d => d.MakhNavigation).WithMany(p => p.Thuonghieusothiches)
                .HasForeignKey(d => d.Makh)
                .HasConstraintName("FK_THST_KH");
        });
        base.OnModelCreating(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
