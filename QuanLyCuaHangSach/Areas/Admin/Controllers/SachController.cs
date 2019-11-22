using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyCuaHangSach.Data;
using QuanLyCuaHangSach.Models;
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


        //GET : Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SachVM.Sach = await _db.Sach.Include(m => m.NhaXuatBan).Include(m => m.TacGia).Include(m => m.TheLoai).SingleOrDefaultAsync(m => m.IDSach == id);

            if (SachVM.Sach == null)
            {
                return NotFound();
            }

            return View(SachVM);
        }


        //Post : Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            if (ModelState.IsValid)
            {
                string webRootPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var sachFromDb = _db.Sach.Where(m => m.IDSach == SachVM.Sach.IDSach).FirstOrDefault();

                if (files.Count > 0 && files[0] != null)
                {
                    //if user uploads a new image
                    var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                    var extension_new = Path.GetExtension(files[0].FileName);
                    var extension_old = Path.GetExtension(sachFromDb.Anh);

                    if (System.IO.File.Exists(Path.Combine(uploads, SachVM.Sach.IDSach + extension_old)))
                    {
                        System.IO.File.Delete(Path.Combine(uploads, SachVM.Sach.IDSach + extension_old));
                    }
                    using (var filestream = new FileStream(Path.Combine(uploads, SachVM.Sach.IDSach + extension_new), FileMode.Create)) 
                    {
                        files[0].CopyTo(filestream);
                    }
                    SachVM.Sach.Anh = @"\" + SD.ImageFolder + @"\" + SachVM.Sach.IDSach + extension_new;
                }

                if (SachVM.Sach.Anh != null)
                {
                    sachFromDb.Anh = SachVM.Sach.Anh;
                }

                sachFromDb.TenSach = SachVM.Sach.TenSach;
                sachFromDb.Gia = SachVM.Sach.Gia;
                sachFromDb.NamXB = SachVM.Sach.NamXB;
                //sachFromDb.CoSan = SachVM.Sach.CoSan;
                sachFromDb.IDTheLoai = SachVM.Sach.IDTheLoai;
                sachFromDb.IDTacGia = SachVM.Sach.IDTacGia;
                sachFromDb.IDNhaXuatBan = SachVM.Sach.IDNhaXuatBan;
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(SachVM);
        }


        //GET : Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SachVM.Sach = await _db.Sach.Include(m => m.NhaXuatBan).Include(m => m.TacGia).Include(m => m.TheLoai).SingleOrDefaultAsync(m => m.IDSach == id);

            if (SachVM.Sach == null)
            {
                return NotFound();
            }

            return View(SachVM);
        }

        //GET : Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            SachVM.Sach = await _db.Sach.Include(m => m.NhaXuatBan).Include(m => m.TacGia).Include(m => m.TheLoai).SingleOrDefaultAsync(m => m.IDSach == id);

            if (SachVM.Sach == null)
            {
                return NotFound();
            }

            return View(SachVM);
        }

        //POST : Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            string webRootPath = _hostingEnvironment.WebRootPath;
            Sach sach = await _db.Sach.FindAsync(id);

            if (sach == null)
            {
                return NotFound();
            }
            else
            {
                var uploads = Path.Combine(webRootPath, SD.ImageFolder);
                var extension = Path.GetExtension(sach.Anh);

                if (System.IO.File.Exists(Path.Combine(uploads, sach.IDSach + extension)))
                {
                    System.IO.File.Delete(Path.Combine(uploads, sach.IDSach + extension));
                }
                _db.Sach.Remove(sach);
                await _db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}