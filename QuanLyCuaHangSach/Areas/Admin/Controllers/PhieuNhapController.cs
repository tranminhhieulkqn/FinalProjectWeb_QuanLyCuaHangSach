using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCuaHangSach.Data;
using QuanLyCuaHangSach.Models;
using QuanLyCuaHangSach.Models.ViewModel;
using QuanLyCuaHangSach.Utility;

namespace QuanLyCuaHangSach.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = SD.AdminEndUser + "," + SD.SuperAdminEndUser)]
    public class PhieuNhapController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public PhieuNhapViewModel PhieuNhapVM { get; set; }
        public PhieuNhapController(ApplicationDbContext db)
        {
            this._db = db;
            PhieuNhapVM = new PhieuNhapViewModel()
            {
                SachVM = new SachViewModel()
                {
                    NhaXuatBan = _db.NhaXuatBan.ToList(),
                    TacGia = _db.TacGia.ToList(),
                    TheLoai = _db.TheLoai.ToList(),
                    Sach = new Models.Sach()
                },
                NhanVien = _db.ApplicationUser.ToList(),
                PhieuNhap = new PhieuNhap()
            };
        }
        public async Task<IActionResult> Index(string searchName = null, string searchCategory = null, string searchAuthor = null, string searchPublishing = null)
        {
            List<Sach> SachVM = new List<Sach>();
            SachVM = _db.Sach.Include(a => a.TheLoai).Include(a => a.TacGia).Include(a => a.NhaXuatBan).ToList();
            if (searchName != null)
            {
                SachVM = SachVM.Where(a => a.TenSach.ToLower().Contains(searchName.ToLower())).ToList();
            }
            if (searchCategory != null)
            {
                SachVM = SachVM.Where(a => a.TheLoai.TenTheLoai.ToLower().Contains(searchCategory.ToLower())).ToList();
            }
            if (searchAuthor != null)
            {
                SachVM = SachVM.Where(a => a.TacGia.TenTacGia.ToLower().Contains(searchAuthor.ToLower())).ToList();
            }
            if (searchPublishing != null)
            {
                SachVM = SachVM.Where(a => a.NhaXuatBan.TenNhaXuatBan.ToLower().Contains(searchPublishing.ToLower())).ToList();
            }
            return View(SachVM);
        }
        //Get: Tao Phieu Nhap moi
        public IActionResult AddAvailable(int? id)
        {
            PhieuNhapVM.PhieuNhap.Sach = _db.Sach.Where(a => a.IDSach == id).FirstOrDefault();
            PhieuNhapVM.PhieuNhap.IDSach = PhieuNhapVM.PhieuNhap.Sach.IDSach;
            return View(PhieuNhapVM);
        }
        //Post: Tao Sach moi
        [HttpPost, ActionName("AddAvailable")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddAvailable()
        {
            if (!ModelState.IsValid)
            {
                return View(PhieuNhapVM);
            }
            //Thay đổi số lượng trên database
            var sachDB = _db.Sach.ToList();
            int iDSach = PhieuNhapVM.PhieuNhap.IDSach;
            int sLSach = PhieuNhapVM.PhieuNhap.SoLuong;
            for (int i = 0; i < sachDB.Count(); i++)
            {
                if (iDSach == sachDB[i].IDSach)
                {
                    sachDB[i].SoLuongCoSan += sLSach; break;
                }
            }
            _db.SaveChanges();
            _db.PhieuNhap.Add(PhieuNhapVM.PhieuNhap);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}