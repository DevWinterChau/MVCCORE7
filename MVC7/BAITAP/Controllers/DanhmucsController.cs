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

namespace BAITAP.Controllers
{
    [Authorize]

    public class DanhmucsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DanhmucsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Danhmucs
        public async Task<IActionResult> Index()
        {
              return _context.Danhmucs != null ? 
                          View(await _context.Danhmucs.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Danhmucs'  is null.");
        }

        // GET: Danhmucs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Danhmucs == null)
            {
                return NotFound();
            }

            var danhmuc = await _context.Danhmucs
                .FirstOrDefaultAsync(m => m.MaDm == id);
            if (danhmuc == null)
            {
                return NotFound();
            }

            return View(danhmuc);
        }

        // GET: Danhmucs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Danhmucs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDm,Ten")] Danhmuc danhmuc)
        {
            if (ModelState.IsValid)
            {
                _context.Add(danhmuc);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(danhmuc);
        }

        // GET: Danhmucs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Danhmucs == null)
            {
                return NotFound();
            }

            var danhmuc = await _context.Danhmucs.FindAsync(id);
            if (danhmuc == null)
            {
                return NotFound();
            }
            return View(danhmuc);
        }

        // POST: Danhmucs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDm,Ten")] Danhmuc danhmuc)
        {
            if (id != danhmuc.MaDm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(danhmuc);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DanhmucExists(danhmuc.MaDm))
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
            return View(danhmuc);
        }

        // GET: Danhmucs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Danhmucs == null)
            {
                return NotFound();
            }

            var danhmuc = await _context.Danhmucs
                .FirstOrDefaultAsync(m => m.MaDm == id);
            if (danhmuc == null)
            {
                return NotFound();
            }

            return View(danhmuc);
        }

        // POST: Danhmucs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Danhmucs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Danhmucs'  is null.");
            }
            var danhmuc = await _context.Danhmucs.FindAsync(id);
            if (danhmuc != null)
            {
                _context.Danhmucs.Remove(danhmuc);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DanhmucExists(int id)
        {
          return (_context.Danhmucs?.Any(e => e.MaDm == id)).GetValueOrDefault();
        }
    }
}
