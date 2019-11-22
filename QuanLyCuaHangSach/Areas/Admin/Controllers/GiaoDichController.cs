using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCuaHangSach.Data;
using QuanLyCuaHangSach.Models;
using QuanLyCuaHangSach.Models.ViewModel;
using QuanLyCuaHangSach.Utility;

namespace QuanLyCuaHangSach.Areas.Customer.Controllers
{
    [Area("Admin"), Authorize(Roles = SD.AdminEndUser + "," + SD.SuperAdminEndUser)]
    public class GiaoDichController : Controller
    {
        private readonly ApplicationDbContext _db;
        private int PageSize = 3;
        public GiaoDichController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public async Task<IActionResult> Index(int productPage = 1, string searchName = null, string searchEmail = null, string searchPhone = null, string searchDate = null)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            GiaoDichViewModel giaoDichVM = new GiaoDichViewModel()
            {
                GiaoDich = new List<Models.GiaoDich>()
            };

            StringBuilder param = new StringBuilder();
            param.Append("/Admin/GiaoDich?productPage=:");
            param.Append("&searchName=");
            if (searchName != null)
            {
                param.Append(searchName);
            }
            param.Append("&searchEmail=");
            if (searchEmail != null)
            {
                param.Append(searchEmail);
            }
            param.Append("&searchPhone=");
            if (searchPhone != null)
            {
                param.Append(searchPhone);
            }
            param.Append("&searchDate=");
            if (searchDate != null)
            {
                param.Append(searchDate);
            }

            giaoDichVM.GiaoDich = _db.GiaoDich.Include(a => a.NguoiBan).Include(a => a.KhachHang).ToList();


            if (User.IsInRole(SD.AdminEndUser))
            {
                giaoDichVM.GiaoDich = giaoDichVM.GiaoDich.Where(a => a.IDNguoiBan == claim.Value).ToList();
            }

            if (searchName != null)
            {
                giaoDichVM.GiaoDich = giaoDichVM.GiaoDich.Where(a => a.KhachHang.TenKhachHang.ToLower().Contains(searchName.ToLower())).ToList();
            }
            if (searchEmail != null)
            {
                giaoDichVM.GiaoDich = giaoDichVM.GiaoDich.Where(a => a.KhachHang.EmailKhachHang.ToLower().Contains(searchEmail.ToLower())).ToList();
            }
            if (searchPhone != null)
            {
                giaoDichVM.GiaoDich = giaoDichVM.GiaoDich.Where(a => a.KhachHang.SDTKhachHang.ToLower().Contains(searchPhone.ToLower())).ToList();
            }
            if (searchDate != null)
            {
                try
                {
                    DateTime appDate = Convert.ToDateTime(searchDate);
                    giaoDichVM.GiaoDich = giaoDichVM.GiaoDich.Where(a => a.NgayGiaoDich.ToShortDateString().Equals(appDate.ToShortDateString())).ToList();
                }
                catch (Exception ex)
                {

                }

            }

            var count = giaoDichVM.GiaoDich.Count;

            giaoDichVM.GiaoDich = giaoDichVM.GiaoDich.OrderBy(p => p.NgayGiaoDich)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize).ToList();


            giaoDichVM.PagingInfo = new PagingInfo
            {
                CurrentPage = productPage,
                ItemsPerPage = PageSize,
                TotalItems = count,
                urlParam = param.ToString()
            };



            return View(giaoDichVM);
        }


        //GET Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sachList = (IEnumerable<Sach>)(from p in _db.Sach
                                                      join a in _db.ChiTietGiaoDich
                                                      on p.IDSach equals a.IDSach
                                                      where a.IDGiaoDich == id
                                                      select p).Include("TheLoai")
                                                               .Include("TacGia")
                                                               .Include("NhaXuatBan");

            ChiTietGiaoDichViewModel chiTietGiaoDichVM = new ChiTietGiaoDichViewModel()
            {
                GiaoDich = _db.GiaoDich.Include(a => a.NguoiBan).Include(a => a.KhachHang).Include(a => a.ChiTietGiaoDichs).Where(a => a.IDGiaoDich == id).FirstOrDefault(),
                NguoiBan = _db.ApplicationUser.ToList(),
                Sach = sachList.ToList()
            };

            return View(chiTietGiaoDichVM);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ChiTietGiaoDichViewModel chiTietGiaoDichVM)
        {
            if (ModelState.IsValid)
            {
                chiTietGiaoDichVM.GiaoDich.NgayGiaoDich = chiTietGiaoDichVM.GiaoDich.NgayGiaoDich
                                    .AddHours(chiTietGiaoDichVM.GiaoDich.GioGiaoDich.Hour)
                                    .AddMinutes(chiTietGiaoDichVM.GiaoDich.GioGiaoDich.Minute);

                var giaoDich_DB = _db.GiaoDich.Where(a => a.IDGiaoDich == chiTietGiaoDichVM.GiaoDich.IDGiaoDich).FirstOrDefault();

                giaoDich_DB.KhachHang = chiTietGiaoDichVM.GiaoDich.KhachHang;
                giaoDich_DB.NgayGiaoDich = chiTietGiaoDichVM.GiaoDich.NgayGiaoDich;
                giaoDich_DB.XacNhan = chiTietGiaoDichVM.GiaoDich.XacNhan;
                if (User.IsInRole(SD.SuperAdminEndUser))
                {
                    giaoDich_DB.IDNguoiBan = chiTietGiaoDichVM.GiaoDich.IDNguoiBan;
                }
                _db.SaveChanges();

                return RedirectToAction(nameof(Index));


            }

            return View(chiTietGiaoDichVM);
        }

        //GET Detials
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sachList = (IEnumerable<Sach>)(  from p in _db.Sach
                                                    join a in _db.ChiTietGiaoDich
                                                    on p.IDSach equals a.IDSach
                                                    where a.IDGiaoDich == id
                                                    select p).Include("TheLoai")
                                                            .Include("TacGia")
                                                            .Include("NhaXuatBan");

            ChiTietGiaoDichViewModel chiTietGiaoDichVM = new ChiTietGiaoDichViewModel()
            {
                GiaoDich = _db.GiaoDich.Include(a => a.NguoiBan).Where(a => a.IDGiaoDich == id).FirstOrDefault(),
                NguoiBan = _db.ApplicationUser.ToList(),
                Sach = sachList.ToList()
            };

            return View(chiTietGiaoDichVM);

        }


        //GET Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sachList = (IEnumerable<Sach>)(from p in _db.Sach
                                               join a in _db.ChiTietGiaoDich
                                               on p.IDSach equals a.IDSach
                                               where a.IDGiaoDich == id
                                               select p).Include("TheLoai")
                                                            .Include("TacGia")
                                                            .Include("NhaXuatBan");

            ChiTietGiaoDichViewModel chiTietGiaoDichVM = new ChiTietGiaoDichViewModel()
            {
                GiaoDich = _db.GiaoDich.Include(a => a.NguoiBan).Include(a => a.KhachHang).Include(a => a.ChiTietGiaoDichs).Where(a => a.IDGiaoDich == id).FirstOrDefault(),
                NguoiBan = _db.ApplicationUser.ToList(),
                Sach = sachList.ToList()
            };

            return View(chiTietGiaoDichVM);

        }


        //POST Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var giaoDich = await _db.GiaoDich.FindAsync(id);
            var khachHang = await _db.KhachHang.FindAsync(giaoDich.IDKhachHang);

            var tableCTGiaoDich = _db.ChiTietGiaoDich.ToList();

            var tableSach = _db.Sach.ToList();
            foreach (var item in tableCTGiaoDich)
            {
                if (item.IDGiaoDich == giaoDich.IDGiaoDich)
                {
                    var ctGiaoDich = await _db.ChiTietGiaoDich.FindAsync(item.IDCTGiaoDich);
                    //Cập nhật lại số sách trong kho nếu giao dịch không thành công
                    foreach (var sach in tableSach)
                    {
                        if(sach.IDSach == ctGiaoDich.IDSach)
                        {
                            int update = sach.SoLuongCoSan + ctGiaoDich.SoLuongMua;
                            var sachDB = await _db.Sach.FindAsync(ctGiaoDich.IDSach);
                            sachDB.SoLuongCoSan = update;
                            _db.Sach.Update(sachDB);
                        }
                    }

                    _db.ChiTietGiaoDich.Remove(ctGiaoDich);
                }
            }

            //foreach (var item in listCTGiaoDich)
            //{
            //    if(item.IDGiaoDich == giaoDich.IDGiaoDich) { }
            //}
            _db.KhachHang.Remove(khachHang);
            _db.GiaoDich.Remove(giaoDich);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}