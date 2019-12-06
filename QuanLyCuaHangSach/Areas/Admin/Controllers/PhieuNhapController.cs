using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuanLyCuaHangSach.Utility;

namespace QuanLyCuaHangSach.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = SD.AdminEndUser + "," + SD.SuperAdminEndUser)]
    public class PhieuNhapController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}