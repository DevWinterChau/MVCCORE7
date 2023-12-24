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
    [Area("Admin")]
    public class ThuonghieuxController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ThuonghieuxController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Sale")]

        // GET: Thuonghieux
        public async Task<IActionResult> Index()
        {
            return _context.Thuonghieus != null ?
                        View(await _context.Thuonghieus.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Thuonghieus'  is null.");
        }
        [Authorize(Roles = "Admin,Sale")]

        // GET: Thuonghieux/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Thuonghieus == null)
            {
                return NotFound();
            }

            var thuonghieu = await _context.Thuonghieus
                .FirstOrDefaultAsync(m => m.MaThuonghieu == id);
            if (thuonghieu == null)
            {
                return NotFound();
            }

            return View(thuonghieu);
        }
        [Authorize(Roles = "Admin,Sale")]

        // GET: Thuonghieux/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Sale")]

        // POST: Thuonghieux/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaThuonghieu,TenThuonghieu")] Thuonghieu thuonghieu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(thuonghieu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(thuonghieu);
        }
        [Authorize(Roles = "Admin")]

        // GET: Thuonghieux/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Thuonghieus == null)
            {
                return NotFound();
            }

            var thuonghieu = await _context.Thuonghieus.FindAsync(id);
            if (thuonghieu == null)
            {
                return NotFound();
            }
            return View(thuonghieu);
        }
        [Authorize(Roles = "Admin")]

        // POST: Thuonghieux/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaThuonghieu,TenThuonghieu")] Thuonghieu thuonghieu)
        {
            if (id != thuonghieu.MaThuonghieu)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(thuonghieu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ThuonghieuExists(thuonghieu.MaThuonghieu))
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
            return View(thuonghieu);
        }
        [Authorize(Roles = "Admin")]

        // GET: Thuonghieux/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Thuonghieus == null)
            {
                return NotFound();
            }

            var thuonghieu = await _context.Thuonghieus
                .FirstOrDefaultAsync(m => m.MaThuonghieu == id);
            if (thuonghieu == null)
            {
                return NotFound();
            }

            return View(thuonghieu);
        }
        [Authorize(Roles = "Admin")]

        // POST: Thuonghieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Thuonghieus == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Thuonghieus'  is null.");
            }
            var thuonghieu = await _context.Thuonghieus.FindAsync(id);
            if (thuonghieu != null)
            {
                _context.Thuonghieus.Remove(thuonghieu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Sale")]

        private bool ThuonghieuExists(int id)
        {
            return (_context.Thuonghieus?.Any(e => e.MaThuonghieu == id)).GetValueOrDefault();
        }
    }
}
