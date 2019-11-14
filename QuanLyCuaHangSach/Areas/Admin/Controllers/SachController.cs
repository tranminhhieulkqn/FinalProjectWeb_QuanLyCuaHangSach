using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCuaHangSach.Data;
using QuanLyCuaHangSach.Models.ViewModel;
using QuanLyCuaHangSach.Utility;

namespace QuanLyCuaHangSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SachController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;

        [BindProperty]
        public SachViewModel SachVM { get; set; }

        public SachController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            this._db = db;
            this._hostingEnvironment = hostingEnvironment;
            SachVM = new SachViewModel()
            {
                NhaXuatBan = _db.NhaXuatBan.ToList(),
                TacGia = _db.TacGia.ToList(),
                TheLoai = _db.TheLoai.ToList(),
                Sach = new Models.Sach()
            };
        }
        public async Task<IActionResult> Index()
        {
            var sach = _db.Sach.Include(m => m.NhaXuatBan).Include(m => m.TacGia).Include(m => m.TheLoai);
            return View(await sach.ToListAsync());
        }

        //Get: Tao Sach moi
        public IActionResult Create()
        {
            return View(SachVM);
        }

        //Post: Tao Sach moi
        [HttpPost, ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePOST()
        {
            if (!ModelState.IsValid)
            {
                return View(SachVM);
            }
            _db.Sach.Add(SachVM.Sach);
            await _db.SaveChangesAsync();
            //Save image
            string webRootPath = _hostingEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            var sachFromDb = _db.Sach.Find(SachVM.Sach.IDSach);

            if (files.Count != 0) 
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(files[0].FileName);
                using (var filestream = new FileStream(Path.Combine(uploads, SachVM.Sach.IDSach + extension), FileMode.Create))
                {
                    files[0].CopyTo(filestream);

                }
                sachFromDb.Anh = @"\" + SD.ImageFolder + @"\" + SachVM.Sach.IDSach + extension;
            }
            else
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultSachImage);
                System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + SachVM.Sach.IDSach + ".png");
                sachFromDb.Anh = @"\" + SD.ImageFolder + @"\" + SachVM.Sach.IDSach + ".png";
            }
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}