using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuanLyCuaHangSach.Data;
using QuanLyCuaHangSach.Models;

namespace QuanLyCuaHangSach.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhaCungCapController : Controller
    {
        private readonly ApplicationDbContext _db;
        public NhaCungCapController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            return View(_db.NhaCungCap.ToList());
        }

        //GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhaCungCap nhaCungCap)
        {
            if (ModelState.IsValid)
            {
                _db.Add(nhaCungCap);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaCungCap);
        }


        //GET Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _db.NhaCungCap.FindAsync(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }

        //POST Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NhaCungCap nhaCungCap)
        {
            if (id != nhaCungCap.IDNhaCungCap)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(nhaCungCap);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaCungCap);
        }

        //GET Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _db.NhaCungCap.FindAsync(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }

        //GET Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaCungCap = await _db.NhaCungCap.FindAsync(id);
            if (nhaCungCap == null)
            {
                return NotFound();
            }

            return View(nhaCungCap);
        }

        //POST Delete action Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhaCungCap = await _db.NhaCungCap.FindAsync(id);
            _db.NhaCungCap.Remove(nhaCungCap);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}