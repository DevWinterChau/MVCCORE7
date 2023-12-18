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

    [Authorize]
    [Area("Admin")]
    public class ChucvusController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChucvusController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Chucvus
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false)
        {
            var applicationDbContext = await _context.Chucvus.Include(x => x.Nhanviens).ToListAsync();
            var totalItems = applicationDbContext.Count();
            // Filter by keyword if provided
            if (!string.IsNullOrEmpty(keyword))
            {
                applicationDbContext = applicationDbContext.Where(x => x.Ten.Contains(keyword.Trim())).ToList();
            }
            // Apply pagination
            var items = applicationDbContext.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Tính toán các thông tin phân trang
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.keyword = keyword;
            ViewBag.Fill = Fill;
            // Populate the dropdown with categories
            ViewBag.ListDanhMuc = await _context.CtKhuyenMais.ToListAsync();
            return View(items);
        }

        // GET: Chucvus/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Chucvus == null)
            {
                return NotFound();
            }

            var chucvu = await _context.Chucvus.Include(x => x.Nhanviens)
                .FirstOrDefaultAsync(m => m.Macv == id);
            if (chucvu == null)
            {
                return NotFound();
            }

            return View(chucvu);
        }

        // GET: Chucvus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Chucvus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Macv,Ten,Heso")] Chucvu chucvu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chucvu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chucvu);
        }

        // GET: Chucvus/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Chucvus == null)
            {
                return NotFound();
            }

            var chucvu = await _context.Chucvus.FindAsync(id);
            if (chucvu == null)
            {
                return NotFound();
            }
            return View(chucvu);
        }

        // POST: Chucvus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Macv,Ten,Heso")] Chucvu chucvu)
        {
            if (id != chucvu.Macv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chucvu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChucvuExists(chucvu.Macv))
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
            return View(chucvu);
        }

        // GET: Chucvus/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Chucvus == null)
            {
                return NotFound();
            }

            var chucvu = await _context.Chucvus
                .FirstOrDefaultAsync(m => m.Macv == id);
            if (chucvu == null)
            {
                return NotFound();
            }

            return View(chucvu);
        }

        // POST: Chucvus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Chucvus == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Chucvus'  is null.");
            }
            var chucvu = await _context.Chucvus.FindAsync(id);
            if (chucvu != null)
            {
                _context.Chucvus.Remove(chucvu);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChucvuExists(int id)
        {
            return (_context.Chucvus?.Any(e => e.Macv == id)).GetValueOrDefault();
        }
    }
}
