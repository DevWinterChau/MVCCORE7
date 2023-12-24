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
    public class KhachhangsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KhachhangsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Sale")]

        // GET: Khachhangs
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false)
        {
            var applicationDbContext = await _context.Khachhangs.Include(x => x.Diachis).ToListAsync();
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
            return View(items);
        }
        [Authorize(Roles = "Admin,Sale")]

        // GET: Khachhangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs.Include(x => x.Diachis)
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }
        [Authorize(Roles = "Admin,Sale")]

        // GET: Khachhangs/Create
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin,Sale")]

        // POST: Khachhangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaKh,Ten,Dienthoai,Email,Matkhau")] Khachhang khachhang)
        {
            if (ModelState.IsValid)
            {
                (string hashedPassword, byte[] salt) = HashPasword.HashPasword.HashPasword1(khachhang.Matkhau);
                khachhang.Matkhau = hashedPassword;
                khachhang.Salt = salt;
                _context.Add(khachhang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(khachhang);
        }
        [Authorize(Roles = "Admin,Sale")]

        // GET: Khachhangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang == null)
            {
                return NotFound();
            }
            return View(khachhang);
        }
        [Authorize(Roles = "Admin,Sale")]

        // POST: Khachhangs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaKh,Ten,Dienthoai,Email,Matkhau")] Khachhang khachhang)
        {
            if (id != khachhang.MaKh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    (string hashedPassword, byte[] salt) = HashPasword.HashPasword.HashPasword1(khachhang.Matkhau);

                    khachhang.Matkhau = hashedPassword;
                    khachhang.Salt = salt;
                    _context.Update(khachhang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KhachhangExists(khachhang.MaKh))
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
            return View(khachhang);
        }
        [Authorize(Roles = "Admin")]

        // GET: Khachhangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Khachhangs == null)
            {
                return NotFound();
            }

            var khachhang = await _context.Khachhangs
                .FirstOrDefaultAsync(m => m.MaKh == id);
            if (khachhang == null)
            {
                return NotFound();
            }

            return View(khachhang);
        }
        [Authorize(Roles = "Admin")]

        // POST: Khachhangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Khachhangs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Khachhangs'  is null.");
            }
            var khachhang = await _context.Khachhangs.FindAsync(id);
            if (khachhang != null)
            {
                _context.Khachhangs.Remove(khachhang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Sale")]
        private bool KhachhangExists(int id)
        {
            return (_context.Khachhangs?.Any(e => e.MaKh == id)).GetValueOrDefault();
        }
    }
}
