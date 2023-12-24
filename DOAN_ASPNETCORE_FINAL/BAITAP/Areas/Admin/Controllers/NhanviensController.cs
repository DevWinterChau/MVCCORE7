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
    public class NhanviensController : Controller
    {
        private readonly ApplicationDbContext _context;

        public NhanviensController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin")]

        // GET: Nhanviens
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false)
        {
            var applicationDbContext = await _context.Nhanviens.Include(x => x.MacvNavigation).ToListAsync();
            var totalItems = applicationDbContext.Count();
            // Filter by keyword if provided
            if (!string.IsNullOrEmpty(keyword))
            {
                applicationDbContext = applicationDbContext
                    .Where(x => x.Ten.Contains(keyword.Trim())
                              || x.Email.Contains(keyword.Trim())
                              || x.Dienthoai.Contains(keyword.Trim())
                ).ToList();
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
        [Authorize(Roles = "Admin")]

        // GET: Nhanviens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Nhanviens == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .Include(n => n.MacvNavigation)
                .FirstOrDefaultAsync(m => m.Manv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }
        [Authorize(Roles = "Admin")]

        // GET: Nhanviens/Create
        public IActionResult Create()
        {
            ViewData["Macv"] = new SelectList(_context.Chucvus, "Macv", "Macv");
            return View();
        }
        [Authorize(Roles = "Admin")]

        // POST: Nhanviens/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Manv,Ten,Macv,Dienthoai,Email,Matkhau")] Nhanvien nhanvien)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhanvien);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Macv"] = new SelectList(_context.Chucvus, "Macv", "Macv", nhanvien.Macv);
            return View(nhanvien);
        }
        [Authorize(Roles = "Admin")]

        // GET: Nhanviens/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Nhanviens == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien == null)
            {
                return NotFound();
            }
            ViewData["Macv"] = new SelectList(_context.Chucvus, "Macv", "Macv", nhanvien.Macv);
            return View(nhanvien);
        }
        [Authorize(Roles = "Admin")]

        // POST: Nhanviens/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Manv,Ten,Macv,Dienthoai,Email,Matkhau")] Nhanvien nhanvien)
        {
            if (id != nhanvien.Manv)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhanvien);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhanvienExists(nhanvien.Manv))
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
            ViewData["Macv"] = new SelectList(_context.Chucvus, "Macv", "Macv", nhanvien.Macv);
            return View(nhanvien);
        }
        [Authorize(Roles = "Admin")]

        // GET: Nhanviens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Nhanviens == null)
            {
                return NotFound();
            }

            var nhanvien = await _context.Nhanviens
                .Include(n => n.MacvNavigation)
                .FirstOrDefaultAsync(m => m.Manv == id);
            if (nhanvien == null)
            {
                return NotFound();
            }

            return View(nhanvien);
        }
        [Authorize(Roles = "Admin")]

        // POST: Nhanviens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Nhanviens == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Nhanviens'  is null.");
            }
            var nhanvien = await _context.Nhanviens.FindAsync(id);
            if (nhanvien != null)
            {
                _context.Nhanviens.Remove(nhanvien);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        private bool NhanvienExists(int id)
        {
            return (_context.Nhanviens?.Any(e => e.Manv == id)).GetValueOrDefault();
        }
    }
}
