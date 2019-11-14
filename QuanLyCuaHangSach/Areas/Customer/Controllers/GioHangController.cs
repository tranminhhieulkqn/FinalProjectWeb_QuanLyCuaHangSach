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
            if (gioHang.Count > 0)
            {
                foreach (int item in gioHang)
                {
                    Sach sach = _db.Sach.Include(m => m.TheLoai).Include(m => m.TacGia).Include(m => m.NhaXuatBan).Where(p => p.IDSach == item).FirstOrDefault();
                    GioHangVM.Sach.Add(sach);
                }
            }
            return View(GioHangVM);
        }
    }
}