using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BAITAP.Data;
using BAITAP.Models;

namespace BAITAP.Controllers
{
    public class ChitietKhuyenMaiSanPhamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChitietKhuyenMaiSanPhamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChitietKhuyenMaiSanPham
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CtKhuyenMaiSanPhams.Include(c => c.MaCtkmNavigation).Include(c => c.MamhNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ChitietKhuyenMaiSanPham/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CtKhuyenMaiSanPhams == null)
            {
                return NotFound();
            }

            var ctKhuyenMaiSanPham = await _context.CtKhuyenMaiSanPhams
                .Include(c => c.MaCtkmNavigation)
                .Include(c => c.MamhNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ctKhuyenMaiSanPham == null)
            {
                return NotFound();
            }

            return View(ctKhuyenMaiSanPham);
        }

        // GET: ChitietKhuyenMaiSanPham/Create
        public IActionResult Create()
        {
            ViewData["MaCtkm"] = new SelectList(_context.CtKhuyenMais, "Id", "Id");
            ViewData["Mamh"] = new SelectList(_context.Mathangs, "MaMh", "MaMh");
            return View();
        }

        // POST: ChitietKhuyenMaiSanPham/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaCtkm,Mamh,Phantramkhuyenmai")] CtKhuyenMaiSanPham ctKhuyenMaiSanPham)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ctKhuyenMaiSanPham);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaCtkm"] = new SelectList(_context.CtKhuyenMais, "Id", "Id", ctKhuyenMaiSanPham.MaCtkm);
            ViewData["Mamh"] = new SelectList(_context.Mathangs, "MaMh", "MaMh", ctKhuyenMaiSanPham.Mamh);
            return View(ctKhuyenMaiSanPham);
        }

        // GET: ChitietKhuyenMaiSanPham/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CtKhuyenMaiSanPhams == null)
            {
                return NotFound();
            }

            var ctKhuyenMaiSanPham = await _context.CtKhuyenMaiSanPhams.FindAsync(id);
            if (ctKhuyenMaiSanPham == null)
            {
                return NotFound();
            }
            ViewData["MaCtkm"] = new SelectList(_context.CtKhuyenMais, "Id", "Id", ctKhuyenMaiSanPham.MaCtkm);
            ViewData["Mamh"] = new SelectList(_context.Mathangs, "MaMh", "MaMh", ctKhuyenMaiSanPham.Mamh);
            return View(ctKhuyenMaiSanPham);
        }

        // POST: ChitietKhuyenMaiSanPham/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MaCtkm,Mamh,Phantramkhuyenmai")] CtKhuyenMaiSanPham ctKhuyenMaiSanPham)
        {
            if (id != ctKhuyenMaiSanPham.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ctKhuyenMaiSanPham);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CtKhuyenMaiSanPhamExists(ctKhuyenMaiSanPham.Id))
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
            ViewData["MaCtkm"] = new SelectList(_context.CtKhuyenMais, "Id", "Id", ctKhuyenMaiSanPham.MaCtkm);
            ViewData["Mamh"] = new SelectList(_context.Mathangs, "MaMh", "MaMh", ctKhuyenMaiSanPham.Mamh);
            return View(ctKhuyenMaiSanPham);
        }

        // GET: ChitietKhuyenMaiSanPham/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CtKhuyenMaiSanPhams == null)
            {
                return NotFound();
            }

            var ctKhuyenMaiSanPham = await _context.CtKhuyenMaiSanPhams
                .Include(c => c.MaCtkmNavigation)
                .Include(c => c.MamhNavigation)
                .FirstOrDefaultAsync(m => m.MaCtkm == id);
            if (ctKhuyenMaiSanPham == null)
            {
                return NotFound();
            }

            return View(ctKhuyenMaiSanPham);
        }

        // POST: ChitietKhuyenMaiSanPham/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CtKhuyenMaiSanPhams == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CtKhuyenMaiSanPhams'  is null.");
            }
            var ctKhuyenMaiSanPham = await _context.CtKhuyenMaiSanPhams.FirstOrDefaultAsync(x => x.MaCtkm.Equals(id));
            if (ctKhuyenMaiSanPham != null)
            {
                _context.CtKhuyenMaiSanPhams.Remove(ctKhuyenMaiSanPham);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CtKhuyenMaiSanPhamExists(int id)
        {
          return (_context.CtKhuyenMaiSanPhams?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
