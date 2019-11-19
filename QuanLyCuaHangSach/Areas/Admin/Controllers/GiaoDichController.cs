using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCuaHangSach.Data;
using QuanLyCuaHangSach.Models.ViewModel;
using QuanLyCuaHangSach.Utility;

namespace QuanLyCuaHangSach.Areas.Customer.Controllers
{
    [Area("Admin"), Authorize(Roles = SD.AdminEndUser + "," + SD.SuperAdminEndUser)]
    public class GiaoDichController : Controller
    {
        private readonly ApplicationDbContext _db;
        public GiaoDichController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public async Task<IActionResult> Index(string searchName = null, string searchEmail = null, string searchPhone = null, string searchDate = null)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            GiaoDichViewModel giaoDichVM = new GiaoDichViewModel()
            {
                GiaoDich = new List<Models.GiaoDich>()
            };

            giaoDichVM.GiaoDich = _db.GiaoDich.Include(a => a.NguoiBan).ToList();
            if (User.IsInRole(SD.AdminEndUser))
            {
                giaoDichVM.GiaoDich = giaoDichVM.GiaoDich.Where(a => a.IDNguoiBan == claim.Value).ToList();
            }

            if (searchName != null)
            {
                giaoDichVM.GiaoDich = giaoDichVM.GiaoDich.Where(a => a.TenKhachHang.ToLower().Contains(searchName.ToLower())).ToList();
            }
            if (searchEmail != null)
            {
                giaoDichVM.GiaoDich = giaoDichVM.GiaoDich.Where(a => a.EmailKhachHang.ToLower().Contains(searchEmail.ToLower())).ToList();
            }
            if (searchPhone != null)
            {
                giaoDichVM.GiaoDich = giaoDichVM.GiaoDich.Where(a => a.SDTKhachHang.ToLower().Contains(searchPhone.ToLower())).ToList();
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


            

            return View(giaoDichVM);
        }
    }
}