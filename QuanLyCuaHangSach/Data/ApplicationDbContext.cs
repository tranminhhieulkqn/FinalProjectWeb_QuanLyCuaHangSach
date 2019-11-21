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
        public DbSet<KhachHang> KhachHang { get; set; }
        public DbSet<Sach> Sach { get; set; }
        public DbSet<GiaoDich> GiaoDich { get; set; }
        public DbSet<ChiTietGiaoDich> ChiTietGiaoDich { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
    }
}
