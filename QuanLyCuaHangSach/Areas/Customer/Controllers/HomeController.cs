using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuanLyCuaHangSach.Data;
using QuanLyCuaHangSach.Extensions;
using QuanLyCuaHangSach.Models;

namespace QuanLyCuaHangSach.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public HomeController(ApplicationDbContext db)
        {
            this._db = db;
        }

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public async Task<IActionResult> Index()
        {
            var sachList = await _db.Sach.Include(m => m.TheLoai).Include(m => m.TacGia).Include(m => m.NhaXuatBan).ToListAsync();
            return View(sachList);
        }

        public async Task<IActionResult> Details(int id)
        {
            var sach = await _db.Sach.Include(m => m.TheLoai).Include(m => m.TacGia).Include(m => m.NhaXuatBan).Where(m => m.IDSach == id).FirstOrDefaultAsync();
            return View(sach);
        }

        [HttpPost, ActionName("Details"), ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsPOST(int id, int soLuongMua)
        {
            List<SoLuong_Sach> gioHang = HttpContext.Session.Get<List<SoLuong_Sach>>("ssGioHang");
            if (gioHang == null)
            {
                gioHang = new List<SoLuong_Sach>();
            }
            gioHang.Add(new SoLuong_Sach() { IDSach = id, SoLuongMua = soLuongMua});
            HttpContext.Session.Set("ssGioHang", gioHang);
            return RedirectToAction("Index", "Home", new { area = "Customer" });
        }

        public IActionResult Remove(int id)
        {
            List<SoLuong_Sach> gioHang = HttpContext.Session.Get<List<SoLuong_Sach>>("ssGioHang");
            if (gioHang.Count > 0)
            {
                foreach (SoLuong_Sach item in gioHang)
                {
                    if(item.IDSach == id)
                    {
                        gioHang.Remove(item); break;
                    }
                }
            }
            HttpContext.Session.Set("ssGioHang", gioHang);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
