using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BAITAP.Data;
using BAITAP.Models;
using Microsoft.AspNetCore.Authorization;

namespace BAITAP.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Sale")]
    [Area("Admin")]
    public class CthoadonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CthoadonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Cthoadons

        // GET: Admin/Cthoadons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cthoadons == null)
            {
                return NotFound();
            }

            var cthoadon = await _context.Cthoadons
                .Include(c => c.MahdNavigation)
                .Include(c => c.MamhNavigation)
                .FirstOrDefaultAsync(m => m.Macthd == id);
            if (cthoadon == null)
            {
                return NotFound();
            }

            return View(cthoadon);
        }

   

        // GET: Admin/Cthoadons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cthoadons == null)
            {
                return NotFound();
            }

            var cthoadon = await _context.Cthoadons.FindAsync(id);
            if (cthoadon == null)
            {
                return NotFound();
            }
            ViewData["Mahd"] = new SelectList(_context.Hoadons.Where(x => x.Mahd.Equals(cthoadon.Mahd)), "Mahd", "Mahd", cthoadon.Mahd);
            ViewData["Mamh"] = new SelectList(_context.Mathangs.Where(x => x.MaMh.Equals(cthoadon.Mamh)), "MaMh", "Ten", cthoadon.Mamh);
            return View(cthoadon);
        }

        // POST: Admin/Cthoadons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Macthd,Mahd,Mamh,Dongia,Soluong,Thanhtien")] Cthoadon cthoadon)
        {
            if (id != cthoadon.Macthd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cthoadon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CthoadonExists(cthoadon.Macthd))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Mahd"] = new SelectList(_context.Hoadons.Where(x => x.Mahd.Equals(cthoadon.Mahd)), "Mahd", "Mahd", cthoadon.Mahd);
            ViewData["Mamh"] = new SelectList(_context.Mathangs.Where(x => x.MaMh.Equals(cthoadon.Mamh)), "MaMh", "Ten", cthoadon.Mamh);

            // Lấy giá trị của Mahd từ cthoadon
            int mahd = cthoadon.Mahd;
            // Tạo một đối tượng RouteValueDictionary để chứa thông tin chuyển hướng
            var routeValues = new RouteValueDictionary
            {
                { "controller", "HoaDons" },   // Controller muốn chuyển hướng đến
                { "action", "Details" },       // Action muốn chuyển hướng đến
                { "id", mahd }                  // Tham số id
            };

            // Sử dụng RedirectToAction với đối số routeValues
            return RedirectToAction("Details", "HoaDons", routeValues);
        }

        // GET: Admin/Cthoadons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cthoadons == null)
            {
                return NotFound();
            }

            var cthoadon = await _context.Cthoadons
                .Include(c => c.MahdNavigation)
                .Include(c => c.MamhNavigation)
                .FirstOrDefaultAsync(m => m.Macthd == id);
            if (cthoadon == null)
            {
                return NotFound();
            }


            return View(cthoadon);
        }

        // POST: Admin/Cthoadons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cthoadons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Cthoadons'  is null.");
            }
            var cthoadon = await _context.Cthoadons.FindAsync(id);
            if (cthoadon != null)
            {
                _context.Cthoadons.Remove(cthoadon);
            }
            int mahd = cthoadon.Mahd;

            await _context.SaveChangesAsync();
            // Lấy giá trị của Mahd từ cthoadon

            // Tạo một đối tượng RouteValueDictionary để chứa thông tin chuyển hướng
            var routeValues = new RouteValueDictionary
            {
                { "controller", "HoaDons" },   // Controller muốn chuyển hướng đến
                { "action", "Details" },       // Action muốn chuyển hướng đến
                { "id", mahd }                  // Tham số id
            };

            // Sử dụng RedirectToAction với đối số routeValues
            return RedirectToAction("Details", "HoaDons", routeValues);
        }

        private bool CthoadonExists(int id)
        {
          return (_context.Cthoadons?.Any(e => e.Macthd == id)).GetValueOrDefault();
        }
    }
}
