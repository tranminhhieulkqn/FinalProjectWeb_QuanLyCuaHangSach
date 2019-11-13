using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuanLyCuaHangSach.Models;

namespace QuanLyCuaHangSach.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<TacGia> TacGia { get; set; }
        public DbSet<TheLoai> TheLoai { get; set; }
        public DbSet<NhaXuatBan> NhaXuatBan { get; set; }
        public DbSet<NhaCungCap> NhaCungCap { get; set; }
        public DbSet<NhanVien> NhanVien { get; set; }
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<PhieuNhap> PhieuNhap { get; set; }
        public DbSet<PhieuXuat> PhieuXuat { get; set; }
        public DbSet<CTPhieuNhap> CTPhieuNhap { get; set; }
        public DbSet<CTPhieuXuat> CTPhieuXuat { get; set; }
        public DbSet<Sach> Sach { get; set; }
    }
}
