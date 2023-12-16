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
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Diachis.Include(d => d.MakhNavigation);
            return View(await applicationDbContext.ToListAsync());
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
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh");
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
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", diachi.Makh);
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
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", diachi.Makh);
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
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "MaKh", diachi.Makh);
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