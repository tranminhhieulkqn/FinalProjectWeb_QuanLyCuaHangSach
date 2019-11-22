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
            List<SoLuong_Sach> gioHang = HttpContext.Session.Get<List<SoLuong_Sach>>("ssGioHang");
            if (gioHang?.Count > 0)
            {
                foreach (SoLuong_Sach item in gioHang)
                {
                    Sach sach = _db.Sach.Include(m => m.TheLoai).Include(m => m.TacGia).Include(m => m.NhaXuatBan).Where(p => p.IDSach == item.IDSach).FirstOrDefault();
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
            List<SoLuong_Sach> gioHang = HttpContext.Session.Get<List<SoLuong_Sach>>("ssGioHang");

            GioHangVM.GiaoDich.NgayGiaoDich = GioHangVM.GiaoDich.NgayGiaoDich
                                                            .AddHours(GioHangVM.GiaoDich.GioGiaoDich.Hour)
                                                            .AddMinutes(GioHangVM.GiaoDich.GioGiaoDich.Minute);



            //Add Khach Hang
            KhachHang khachHang = GioHangVM.GiaoDich.KhachHang;
            _db.KhachHang.Add(khachHang);
            _db.SaveChanges();

            int idKhachHang = khachHang.IDKhachHang;

            //Add Giao Dich
            GiaoDich giaoDich = GioHangVM.GiaoDich;
            giaoDich.IDKhachHang = idKhachHang;
            _db.GiaoDich.Add(giaoDich);
            _db.SaveChanges();

            int appointmentId = giaoDich.IDGiaoDich;

            var sachDB = _db.Sach.ToList();

            foreach (SoLuong_Sach item in gioHang)
            {
                ChiTietGiaoDich chiTietGiaoDich = new ChiTietGiaoDich()
                {
                    IDGiaoDich = appointmentId,
                    IDSach = item.IDSach,
                    SoLuongMua = item.SoLuongMua
                };
                for (int i = 0; i < sachDB.Count(); i++)
                {
                    if (item.IDSach == sachDB[i].IDSach)
                    {
                        sachDB[i].SoLuongCoSan -= item.SoLuongMua; break;
                    }
                }
                _db.SaveChanges();
                _db.ChiTietGiaoDich.Add(chiTietGiaoDich);

            }
            _db.SaveChanges();
            gioHang = new List<SoLuong_Sach>();
            HttpContext.Session.Set("ssGioHang", gioHang);


            return RedirectToAction("GiaoDichConfirmation", "GioHang", new { Id = appointmentId });
            //return RedirectToAction("Index");

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


        // Get
        public IActionResult GiaoDichConfirmation(int id)
        {
            GiaoDich giaoDich = _db.GiaoDich.Where(a => a.IDGiaoDich == id).FirstOrDefault();
            giaoDich.KhachHang = _db.KhachHang.Where(a => a.IDKhachHang == giaoDich.IDKhachHang).FirstOrDefault();
            List<ChiTietGiaoDich> cTGiaoDich = _db.ChiTietGiaoDich.Where(p => p.IDGiaoDich == id).ToList();

            List<Sach> listSach = new List<Sach>();
            foreach (ChiTietGiaoDich item in cTGiaoDich)
            {
                listSach.Add(_db.Sach.Include(p => p.TheLoai).Include(p => p.TacGia).Include(p => p.NhaXuatBan).Where(p => p.IDSach == item.IDSach).FirstOrDefault());
            }
            ChiTietGiaoDichViewModel chiTietGiaoDichVM = new ChiTietGiaoDichViewModel()
            {
                GiaoDich = _db.GiaoDich.Where(a => a.IDGiaoDich == id).FirstOrDefault(),
                Sach = listSach
            };
            
            //GioHangVM.GiaoDich = _db.GiaoDich.Where(a => a.IDGiaoDich == id).FirstOrDefault();
            //GioHangVM.GiaoDich.KhachHang = _db.KhachHang.Where(a => a.IDKhachHang == GioHangVM.GiaoDich.IDKhachHang).FirstOrDefault();
            //List<ChiTietGiaoDich> cTGiaoDich = _db.ChiTietGiaoDich.Where(p => p.IDGiaoDich == id).ToList();
            
            //foreach (ChiTietGiaoDich item in cTGiaoDich)
            //{
            //    GioHangVM.Sach.Add(_db.Sach.Include(p => p.TheLoai).Include(p => p.TacGia).Include(p => p.NhaXuatBan).Where(p => p.IDSach == item.IDSach).FirstOrDefault());
            //}

            return View(chiTietGiaoDichVM);
        }
    }
}