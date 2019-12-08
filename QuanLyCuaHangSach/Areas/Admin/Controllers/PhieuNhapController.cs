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
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace QuanLyCuaHangSach.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = SD.AdminEndUser + "," + SD.SuperAdminEndUser)]
    public class PhieuNhapController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;
        [BindProperty]
        public PhieuNhapViewModel PhieuNhapVM { get; set; }
        public PhieuNhapController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            this._db = db;
            this._hostingEnvironment = hostingEnvironment;
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
        public async Task<IActionResult> Index()
        {
            List<PhieuNhap> listPhieuNhap = new List<PhieuNhap>();
            listPhieuNhap = _db.PhieuNhap.Include(s => s.Sach).Include(n => n.NhanVien).ToList();
            return View(listPhieuNhap);
        }
        // Show toàn bộ các phiếu nhập hàng
        public async Task<IActionResult> SearchSach(string searchName = null, string searchCategory = null, string searchAuthor = null, string searchPublishing = null)
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
            return RedirectToAction(nameof(SearchSach));
        }

        //Get: Tao Phieu Nhap moi
        public IActionResult AddUnavailable()
        {
            return View(PhieuNhapVM);
        }
        //Post: Tao Sach moi
        [HttpPost, ActionName("AddUnavailable")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddUnavailablePOST()
        {
            if (!ModelState.IsValid)
            {
                return View(PhieuNhapVM);
            }
            //KhachHang khachHang = GioHangVM.GiaoDich.KhachHang;
            //_db.KhachHang.Add(khachHang);
            //_db.SaveChanges();

            //int idKhachHang = khachHang.IDKhachHang;

            Sach sach = PhieuNhapVM.SachVM.Sach;
            _db.Sach.Add(sach);
            _db.SaveChanges();
            int iDSach = PhieuNhapVM.SachVM.Sach.IDSach;
            //Save image
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var sachFromDb = _db.Sach.Find(iDSach);

            if (files.Count != 0)
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);
                using (var filestream = new FileStream(Path.Combine(uploads, iDSach + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);

                }
                sachFromDb.Anh = @"\" + SD.ImageFolder + @"\" + iDSach + extension;
            }
            else
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultSachImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + iDSach + ".png");
                sachFromDb.Anh = @"\" + SD.ImageFolder + @"\" + iDSach + ".png";
            }
            await _db.SaveChangesAsync();
            //Thay đổi số lượng trên database
            var sachDB = _db.Sach.ToList();
            int sLSach = PhieuNhapVM.PhieuNhap.SoLuong;
            for (int i = 0; i < sachDB.Count(); i++)
            {
                if (iDSach == sachDB[i].IDSach)
                {
                    sachDB[i].SoLuongCoSan += sLSach; break;
                }
            }
            _db.SaveChanges();
            PhieuNhapVM.PhieuNhap.IDSach = iDSach;
            _db.PhieuNhap.Add(PhieuNhapVM.PhieuNhap);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(SearchSach));
        }
    }
}