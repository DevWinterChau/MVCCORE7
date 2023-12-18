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
    public class DiachisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiachisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diachis
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false)
        {
            var applicationDbContext = await _context.Diachis.Include(x => x.MakhNavigation).ToListAsync();
            var totalItems = applicationDbContext.Count();
            // Filter by keyword if provided
            if (!string.IsNullOrEmpty(keyword))
            {
                applicationDbContext = applicationDbContext.Where(x => x.Diachi1.Contains(keyword.Trim())
                || x.Tinhthanh.Contains(keyword.Trim())
                || x.Phuongxa.Contains(keyword.Trim())
                || x.Quanhuyen.Contains(keyword.Trim())
                || x.MakhNavigation.Ten.Contains(keyword.Trim())
                || x.MakhNavigation.Dienthoai.Contains(keyword.Trim())).ToList();
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


        // GET: Diachis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Diachis == null)
            {
                return NotFound();
            }

            var diachi = await _context.Diachis
                .Include(d => d.MakhNavigation)
                .FirstOrDefaultAsync(m => m.MaDc == id);
            if (diachi == null)
            {
                return NotFound();
            }

            return View(diachi);
        }

        // GET: Diachis/Create
        public IActionResult Create()
        {
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "Ten");
            return View();
        }

        // POST: Diachis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaDc,Makh,Diachi1,Phuongxa,Quanhuyen,Tinhthanh,Macdinh")] Diachi diachi)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diachi);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "Ten", diachi.Makh);
            return View(diachi);
        }

        // GET: Diachis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Diachis == null)
            {
                return NotFound();
            }

            var diachi = await _context.Diachis.FindAsync(id);
            if (diachi == null)
            {
                return NotFound();
            }
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "Ten", diachi.Makh);
            return View(diachi);
        }

        // POST: Diachis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaDc,Makh,Diachi1,Phuongxa,Quanhuyen,Tinhthanh,Macdinh")] Diachi diachi)
        {
            if (id != diachi.MaDc)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diachi);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiachiExists(diachi.MaDc))
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
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "Ten", diachi.Makh);
            return View(diachi);
        }

        // GET: Diachis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Diachis == null)
            {
                return NotFound();
            }

            var diachi = await _context.Diachis
                .Include(d => d.MakhNavigation)
                .FirstOrDefaultAsync(m => m.MaDc == id);
            if (diachi == null)
            {
                return NotFound();
            }

            return View(diachi);
        }

        // POST: Diachis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Diachis == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Diachis'  is null.");
            }
            var diachi = await _context.Diachis.FindAsync(id);
            if (diachi != null)
            {
                _context.Diachis.Remove(diachi);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiachiExists(int id)
        {
            return (_context.Diachis?.Any(e => e.MaDc == id)).GetValueOrDefault();
        }
    }
}
