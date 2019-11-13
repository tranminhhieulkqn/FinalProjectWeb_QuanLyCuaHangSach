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
    public class NhaXuatBanController : Controller
    {
        private readonly ApplicationDbContext _db;
        public NhaXuatBanController(ApplicationDbContext db)
        {
            this._db = db;
        }
        public IActionResult Index()
        {
            return View(_db.NhaXuatBan.ToList());
        }
        //GET Create Action Method
        public IActionResult Create()
        {
            return View();
        }

        //POST Create action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NhaXuatBan nhaXuatBan)
        {
            if (ModelState.IsValid)
            {
                _db.Add(nhaXuatBan);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaXuatBan);
        }


        //GET Edit Action Method
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _db.NhaXuatBan.FindAsync(id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            return View(nhaXuatBan);
        }

        //POST Edit action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, NhaXuatBan nhaXuatBan)
        {
            if (id != nhaXuatBan.IDNhaXuatBan)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _db.Update(nhaXuatBan);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaXuatBan);
        }

        //GET Details Action Method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _db.NhaXuatBan.FindAsync(id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            return View(nhaXuatBan);
        }

        //GET Delete Action Method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaXuatBan = await _db.NhaXuatBan.FindAsync(id);
            if (nhaXuatBan == null)
            {
                return NotFound();
            }

            return View(nhaXuatBan);
        }

        //POST Delete action Method
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhaXuatBan = await _db.NhaXuatBan.FindAsync(id);
            _db.NhaXuatBan.Remove(nhaXuatBan);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}