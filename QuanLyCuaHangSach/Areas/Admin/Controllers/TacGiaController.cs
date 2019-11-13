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
    public class TacGiaController : Controller
    {
        private readonly ApplicationDbContext _db;
        public TacGiaController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            return View(_db.TacGia.ToList());
        }

        //GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TacGia tacGia)
        {
            if (ModelState.IsValid)
            {
                _db.Add(tacGia);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tacGia);
        }


        //GET Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tacGia = await _db.TacGia.FindAsync(id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        //POST Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, TacGia tacGia)
        {
            if (id != tacGia.IDTacGia)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(tacGia);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tacGia);
        }

        //GET Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tacGia = await _db.TacGia.FindAsync(id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        //GET Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tacGia = await _db.TacGia.FindAsync(id);
            if (tacGia == null)
            {
                return NotFound();
            }

            return View(tacGia);
        }

        //POST Delete action Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tacGia = await _db.TacGia.FindAsync(id);
            _db.TacGia.Remove(tacGia);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}