﻿using System;
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
    public class LoaiKhuyenMaisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoaiKhuyenMaisController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Sale")]

        // GET: LoaiKhuyenMais
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false)
        {
            var applicationDbContext = await _context.LoaiKhuyenMais.Include(x => x.CtKhuyenMais).ToListAsync();
            var totalItems = applicationDbContext.Count();
            // Filter by keyword if provided
            if (!string.IsNullOrEmpty(keyword))
            {
                applicationDbContext = applicationDbContext
                    .Where(x => x.TenLoaiKm.Contains(keyword.Trim())
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

        // GET: LoaiKhuyenMais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.LoaiKhuyenMais == null)
            {
                return NotFound();
            }

            var loaiKhuyenMai = await _context.LoaiKhuyenMais.Include(x => x.CtKhuyenMais)
                .FirstOrDefaultAsync(m => m.MaLoaiKm == id);
            if (loaiKhuyenMai == null)
            {
                return NotFound();
            }

            return View(loaiKhuyenMai);
        }

        // GET: LoaiKhuyenMais/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiKhuyenMais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaLoaiKm,TenLoaiKm")] LoaiKhuyenMai loaiKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiKhuyenMai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiKhuyenMai);
        }

        // GET: LoaiKhuyenMais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.LoaiKhuyenMais == null)
            {
                return NotFound();
            }

            var loaiKhuyenMai = await _context.LoaiKhuyenMais.FindAsync(id);
            if (loaiKhuyenMai == null)
            {
                return NotFound();
            }
            return View(loaiKhuyenMai);
        }

        // POST: LoaiKhuyenMais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaLoaiKm,TenLoaiKm")] LoaiKhuyenMai loaiKhuyenMai)
        {
            if (id != loaiKhuyenMai.MaLoaiKm)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiKhuyenMai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiKhuyenMaiExists(loaiKhuyenMai.MaLoaiKm))
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
            return View(loaiKhuyenMai);
        }

        // GET: LoaiKhuyenMais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.LoaiKhuyenMais == null)
            {
                return NotFound();
            }

            var loaiKhuyenMai = await _context.LoaiKhuyenMais
                .FirstOrDefaultAsync(m => m.MaLoaiKm == id);
            if (loaiKhuyenMai == null)
            {
                return NotFound();
            }

            return View(loaiKhuyenMai);
        }

        // POST: LoaiKhuyenMais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.LoaiKhuyenMais == null)
            {
                return Problem("Entity set 'ApplicationDbContext.LoaiKhuyenMais'  is null.");
            }
            var loaiKhuyenMai = await _context.LoaiKhuyenMais.FindAsync(id);
            if (loaiKhuyenMai != null)
            {
                _context.LoaiKhuyenMais.Remove(loaiKhuyenMai);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiKhuyenMaiExists(int id)
        {
            return (_context.LoaiKhuyenMais?.Any(e => e.MaLoaiKm == id)).GetValueOrDefault();
        }
    }
}
