using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QuanLyCuaHangSach.Data;

namespace QuanLyCuaHangSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanVienController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _hostingEnvironment;
        public NhanVienController(ApplicationDbContext db, IWebHostEnvironment hostingEnvironment)
        {
            this._db = db;
            this._hostingEnvironment = hostingEnvironment;
        }
        public IActionResult Index()
        {
            return View(_db.NhanVien.ToList());
        }

        ////Get: Tao Sach moi
        //public IActionResult Create()
        //{
        //    return View(SachVM);
        //}

        ////Post: Tao Sach moi
        //[HttpPost, ActionName("Create")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreatePOST()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(SachVM);
        //    }
        //    _db.Sach.Add(SachVM.Sach);
        //    await _db.SaveChangesAsync();
        //    //Save image
        //    string webRootPath = _hostingEnvironment.WebRootPath;
        //    var files = HttpContext.Request.Form.Files;

        //    var sachFromDb = _db.Sach.Find(SachVM.Sach.IDSach);

        //    if (files.Count != 0)
        //    {
        //        var uploads = Path.Combine(webRootPath, SD.ImageFolder);
        //        var extension = Path.GetExtension(files[0].FileName);
        //        using (var filestream = new FileStream(Path.Combine(uploads, SachVM.Sach.IDSach + extension), FileMode.Create))
        //        {
        //            files[0].CopyTo(filestream);

        //        }
        //        sachFromDb.Anh = @"\" + SD.ImageFolder + @"\" + SachVM.Sach.IDSach + extension;
        //    }
        //    else
        //    {
        //        var uploads = Path.Combine(webRootPath, SD.ImageFolder + @"\" + SD.DefaultSachImage);
        //        System.IO.File.Copy(uploads, webRootPath + @"\" + SD.ImageFolder + @"\" + SachVM.Sach.IDSach + ".png");
        //        sachFromDb.Anh = @"\" + SD.ImageFolder + @"\" + SachVM.Sach.IDSach + ".png";
        //    }
        //    await _db.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}
    }
}