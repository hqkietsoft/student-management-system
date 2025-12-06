using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace QuanLySinhVien_Nhom2.Models
{
    public partial class MyDbContext : DbContext
    {
        public MyDbContext()
        {
        }

        public MyDbContext(DbContextOptions<MyDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Diem> Diem { get; set; }
        public virtual DbSet<GiaoVien> GiaoVien { get; set; }
        public virtual DbSet<Khoa> Khoa { get; set; }
        public virtual DbSet<LopHoc> LopHoc { get; set; }
        public virtual DbSet<LopHocPhan> LopHocPhan { get; set; }
        public virtual DbSet<MonHoc> MonHoc { get; set; }
        public virtual DbSet<NguoiDung> NguoiDung { get; set; }
        public virtual DbSet<SinhVien> SinhVien { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=QUANLYSINHVIEN;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Diem>(entity =>
            {
                entity.HasKey(e => new { e.MaSv, e.MaMh });

                entity.Property(e => e.MaSv)
                    .HasColumnName("MaSV")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaMh)
                    .HasColumnName("MaMH")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DiemCc).HasColumnName("DiemCC");

                entity.Property(e => e.DiemHs1).HasColumnName("DiemHS1");

                entity.Property(e => e.DiemHs2l1).HasColumnName("DiemHS2L1");

                entity.Property(e => e.DiemHs2l2).HasColumnName("DiemHS2L2");

                entity.Property(e => e.DiemTbhk).HasColumnName("DiemTBHK");

                entity.Property(e => e.TenDn)
                    .HasColumnName("TenDN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.Diem)
                    .HasForeignKey(d => d.MaMh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaMHDiem");

                entity.HasOne(d => d.SinhVien)
                    .WithMany(p => p.Diem)
                    .HasForeignKey(d => new { d.MaSv, d.TenDn })
                    .HasConstraintName("FK_MaSVDiem");
            });

            modelBuilder.Entity<GiaoVien>(entity =>
            {
                entity.HasKey(e => new { e.MaGv, e.TenDn });

                entity.Property(e => e.MaGv)
                    .HasColumnName("MaGV")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDn)
                    .HasColumnName("TenDN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HoTen).HasMaxLength(50);

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.HasOne(d => d.MaKhoaNavigation)
                    .WithMany(p => p.GiaoVien)
                    .HasForeignKey(d => d.MaKhoa)
                    .HasConstraintName("FK_MaKhoa");

                entity.HasOne(d => d.TenDnNavigation)
                    .WithMany(p => p.GiaoVien)
                    .HasForeignKey(d => d.TenDn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenDN_GV");
            });

            modelBuilder.Entity<Khoa>(entity =>
            {
                entity.HasKey(e => e.MaKhoa)
                    .HasName("PK__Khoa__65390405DB97438A");

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenKhoa).HasMaxLength(100);
            });

            modelBuilder.Entity<LopHoc>(entity =>
            {
                entity.HasKey(e => e.MaLop)
                    .HasName("PK__LopHoc__3B98D273939897B5");

                entity.Property(e => e.MaLop)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HeDaoTao).HasMaxLength(50);

                entity.Property(e => e.KhoaHoc).HasMaxLength(50);

                entity.Property(e => e.MaKhoa)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NamNhapHoc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenLop).HasMaxLength(50);

                entity.HasOne(d => d.MaKhoaNavigation)
                    .WithMany(p => p.LopHoc)
                    .HasForeignKey(d => d.MaKhoa)
                    .HasConstraintName("FK_KhoaLopHoc");
            });

            modelBuilder.Entity<LopHocPhan>(entity =>
            {
                entity.HasKey(e => new { e.MaLop, e.MaMh });

                entity.Property(e => e.MaLop)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaMh)
                    .HasColumnName("MaMH")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaGv)
                    .HasColumnName("MaGV")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.MaMhNavigation)
                    .WithMany(p => p.LopHocPhan)
                    .HasForeignKey(d => d.MaMh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaMH_LopHocPhan");
            });

            modelBuilder.Entity<MonHoc>(entity =>
            {
                entity.HasKey(e => e.MaMh)
                    .HasName("PK__MonHoc__2725DFD959BCBD6E");

                entity.Property(e => e.MaMh)
                    .HasColumnName("MaMH")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SoTc).HasColumnName("SoTC");

                entity.Property(e => e.TenMh)
                    .HasColumnName("TenMH")
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasKey(e => e.TenDn)
                    .HasName("PK__NguoiDun__4CF96559C7B077E4");

                entity.Property(e => e.TenDn)
                    .HasColumnName("TenDN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoaiNd).HasColumnName("LoaiND");

                entity.Property(e => e.MatKhau)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SinhVien>(entity =>
            {
                entity.HasKey(e => new { e.MaSv, e.TenDn });

                entity.Property(e => e.MaSv)
                    .HasColumnName("MaSV")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TenDn)
                    .HasColumnName("TenDN")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GioiTinh).HasMaxLength(50);

                entity.Property(e => e.HoDem).HasMaxLength(50);

                entity.Property(e => e.MaLop)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.QueQuan).HasMaxLength(50);

                entity.Property(e => e.SoDt)
                    .HasColumnName("SoDT")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Ten).HasMaxLength(50);

                entity.HasOne(d => d.MaLopNavigation)
                    .WithMany(p => p.SinhVien)
                    .HasForeignKey(d => d.MaLop)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MaLop_SV");

                entity.HasOne(d => d.TenDnNavigation)
                    .WithMany(p => p.SinhVien)
                    .HasForeignKey(d => d.TenDn)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TenDN_SV");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
