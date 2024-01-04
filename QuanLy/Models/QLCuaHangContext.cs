using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLy.Models;

namespace QuanLy.Models
{
    public class QLCuaHangContext : DbContext
    {
        public QLCuaHangContext(DbContextOptions<QLCuaHangContext> options): base(options){ }
        
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<NhanVien> NhanViens { get; set; }
        public DbSet<HoaDonNhap> HoaDonNhaps { get; set; }
        public DbSet<CTHDN> cTHDNs { get; set; }
        public DbSet<HoaDonBan> HoaDonBans { get; set; }
        public DbSet<CTHDB> cTHDBs { get; set; }
        public DbSet<LoaiHang>LoaiHangs { get; set; }
        public DbSet<NhaCC> NhaCCs { get; set; }
        public DbSet<SanPham> SanPhams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaiKhoan>().ToTable("TaiKhoan");
            modelBuilder.Entity<NhanVien>().ToTable("NhanVien");
            modelBuilder.Entity<HoaDonNhap>().ToTable("HoaDonNhap");
            modelBuilder.Entity<CTHDN>().ToTable("CTHDN");
            modelBuilder.Entity<HoaDonBan>().ToTable("HoaDonBan");
            modelBuilder.Entity<CTHDB>().ToTable("CTHDB");
            modelBuilder.Entity<LoaiHang>().ToTable("LoaiHang");
            modelBuilder.Entity<NhaCC>().ToTable("NhaCC");
            modelBuilder.Entity<SanPham>().ToTable("SanPham");
        }
    }
}
