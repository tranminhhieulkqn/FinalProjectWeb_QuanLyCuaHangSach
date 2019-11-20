using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCuaHangSach.Data;
using QuanLyCuaHangSach.Extensions;
using QuanLyCuaHangSach.Models;
using QuanLyCuaHangSach.Models.ViewModel;

namespace QuanLyCuaHangSach.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class GioHangController : Controller
    {
        private readonly ApplicationDbContext _db;
        
        [BindProperty]
        public GioHangViewModel GioHangVM { get; set; }
        
        public GioHangController(ApplicationDbContext db)
        {
            this._db = db;
            GioHangVM = new GioHangViewModel()
            {
                Sach = new List<Models.Sach>()
            };
        }
        public async Task<IActionResult> Index()
        {
            List<int> gioHang = HttpContext.Session.Get<List<int>>("ssGioHang");
            if (gioHang?.Count > 0)
            {
                foreach (int item in gioHang)
                {
                    Sach sach = _db.Sach.Include(m => m.TheLoai).Include(m => m.TacGia).Include(m => m.NhaXuatBan).Where(p => p.IDSach == item).FirstOrDefault();
                    GioHangVM.Sach.Add(sach);
                }
            }
            return View(GioHangVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Index")]
        public IActionResult IndexPost()
        {
            List<int> gioHang = HttpContext.Session.Get<List<int>>("ssGioHang");

            GioHangVM.GiaoDich.NgayGiaoDich = GioHangVM.GiaoDich.NgayGiaoDich
                                                            .AddHours(GioHangVM.GiaoDich.GioGiaoDich.Hour)
                                                            .AddMinutes(GioHangVM.GiaoDich.GioGiaoDich.Minute);

            GiaoDich giaoDich = GioHangVM.GiaoDich;
            _db.GiaoDich.Add(giaoDich);
            _db.SaveChanges();

            int appointmentId = giaoDich.IDGiaoDich;

            foreach (int sachID in gioHang)
            {
                ChiTietGiaoDich chiTietGiaoDich = new ChiTietGiaoDich()
                {
                    IDGiaoDich = appointmentId,
                    IDSach = sachID
                };
                _db.ChiTietGiaoDich.Add(chiTietGiaoDich);

            }
            _db.SaveChanges();
            gioHang = new List<int>();
            HttpContext.Session.Set("ssGioHang", gioHang);

            return RedirectToAction("GiaoDichConfirmation", "GioHang", new { Id = appointmentId });
            //return RedirectToAction("Index");

        }

        public IActionResult Remove(int id)
        {
            List<int> gioHang = HttpContext.Session.Get<List<int>>("ssGioHang");

            if (gioHang.Count > 0)
            {
                if (gioHang.Contains(id))
                {
                    gioHang.Remove(id);
                }
            }

            HttpContext.Session.Set("ssGioHang", gioHang);

            return RedirectToAction(nameof(Index));
        }


        // Get
        public IActionResult GiaoDichConfirmation(int id)
        {
            GioHangVM.GiaoDich = _db.GiaoDich.Where(a => a.IDGiaoDich == id).FirstOrDefault();
            List<ChiTietGiaoDich> cTGiaoDich = _db.ChiTietGiaoDich.Where(p => p.IDGiaoDich == id).ToList();

            foreach (ChiTietGiaoDich item in cTGiaoDich)
            {
                GioHangVM.Sach.Add(_db.Sach.Include(p => p.TheLoai).Include(p => p.TacGia).Include(p => p.NhaXuatBan).Where(p => p.IDSach == item.IDSach).FirstOrDefault());
            }

            return View(GioHangVM);
        }
    }
}