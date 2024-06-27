using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace hottoc.Models
{
    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public virtual DbSet<ChucVu> ChucVus { get; set; }
        public virtual DbSet<CongNhanVien> CongNhanViens { get; set; }
        public virtual DbSet<DangNhap> DangNhaps { get; set; }
        public virtual DbSet<DichVu> DichVus { get; set; }
        public virtual DbSet<HinhAnh> HinhAnhs { get; set; }
        public virtual DbSet<HoaDon> HoaDons { get; set; }
        public virtual DbSet<KhachHang> KhachHangs { get; set; }
        public virtual DbSet<LichHen> LichHens { get; set; }
        public virtual DbSet<NhanVien> NhanViens { get; set; }
        public virtual DbSet<SanPhamKem> SanPhamKems { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChiTietHoaDon>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<ChucVu>()
                .Property(e => e.HeSoLuong)
                .HasPrecision(18, 0);

            modelBuilder.Entity<DangNhap>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<DangNhap>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<DichVu>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HinhAnh>()
                .Property(e => e.DuongDan);

            modelBuilder.Entity<HinhAnh>()
                .HasMany(e => e.DichVus)
                .WithOptional(e => e.HinhAnh)
                .HasForeignKey(e => e.IDHinh);

            modelBuilder.Entity<HinhAnh>()
                .HasMany(e => e.SanPhamKems)
                .WithOptional(e => e.HinhAnh)
                .HasForeignKey(e => e.IDHinh);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.ThanhTien)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.KhachTra)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDon>()
                .Property(e => e.ThoiLai)
                .HasPrecision(18, 0);

            modelBuilder.Entity<HoaDon>()
                .HasMany(e => e.ChiTietHoaDons)
                .WithOptional(e => e.HoaDon)
                .HasForeignKey(e => e.IDHoaDon);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.CongNhanViens)
                .WithRequired(e => e.NhanVien)
                .HasForeignKey(e => e.IDNV)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NhanVien>()
                .HasMany(e => e.DangNhaps)
                .WithOptional(e => e.NhanVien)
                .HasForeignKey(e => e.IDNV);

            modelBuilder.Entity<SanPhamKem>()
                .Property(e => e.Gia)
                .HasPrecision(18, 0);
        }
    }
}
