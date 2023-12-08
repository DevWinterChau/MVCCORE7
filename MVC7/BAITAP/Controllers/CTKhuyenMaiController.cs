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
    public class CTKhuyenMaiController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CTKhuyenMaiController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CTKhuyenMai
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.CtKhuyenMais.Include(c => c.MaLoaiKmNavigation).Include(c => c.NhomSpkhuyemaiNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: CTKhuyenMai/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CtKhuyenMais == null)
            {
                return NotFound();
            }

            var ctKhuyenMai = await _context.CtKhuyenMais
                .Include(c => c.MaLoaiKmNavigation)
                .Include(c => c.NhomSpkhuyemaiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ctKhuyenMai == null)
            {
                return NotFound();
            }

            return View(ctKhuyenMai);
        }

        // GET: CTKhuyenMai/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["MaLoaiKm"] = new SelectList(_context.LoaiKhuyenMais, "MaLoaiKm", "TenLoaiKm");
                ViewData["NhomSpkhuyemai"] = new SelectList(_context.Danhmucs, "MaDm", "Ten");
                return View();
            }
            catch(Exception ex)
            {

            }
            return BadRequest();
        }

        // POST: CTKhuyenMai/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TenKm,MoTa,NgayBatDau,NgayKetThuc,Soluongmuatoithieu,Sotienmuatoithieu,PhanTramGiamGia,GiaGiam,DieuKienApDung,Soluongsudung,NhomSpkhuyemai,TrangThai,MaLoaiKm")] CtKhuyenMai ctKhuyenMai)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ctKhuyenMai);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MaLoaiKm"] = new SelectList(_context.LoaiKhuyenMais, "MaLoaiKm", "MaLoaiKm", ctKhuyenMai.MaLoaiKm);
            ViewData["NhomSpkhuyemai"] = new SelectList(_context.Danhmucs, "MaDm", "MaDm", ctKhuyenMai.NhomSpkhuyemai);
            return View(ctKhuyenMai);
        }

        // GET: CTKhuyenMai/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CtKhuyenMais == null)
            {
                return NotFound();
            }

            var ctKhuyenMai = await _context.CtKhuyenMais.FindAsync(id);
            if (ctKhuyenMai == null)
            {
                return NotFound();
            }
            ViewData["MaLoaiKm"] = new SelectList(_context.LoaiKhuyenMais, "MaLoaiKm", "MaLoaiKm", ctKhuyenMai.MaLoaiKm);
            ViewData["NhomSpkhuyemai"] = new SelectList(_context.Danhmucs, "MaDm", "MaDm", ctKhuyenMai.NhomSpkhuyemai);
            return View(ctKhuyenMai);
        }

        // POST: CTKhuyenMai/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TenKm,MoTa,NgayBatDau,NgayKetThuc,Soluongmuatoithieu,Sotienmuatoithieu,PhanTramGiamGia,GiaGiam,DieuKienApDung,Soluongsudung,NhomSpkhuyemai,TrangThai,MaLoaiKm")] CtKhuyenMai ctKhuyenMai)
        {
            if (id != ctKhuyenMai.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ctKhuyenMai);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CtKhuyenMaiExists(ctKhuyenMai.Id))
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
            ViewData["MaLoaiKm"] = new SelectList(_context.LoaiKhuyenMais, "MaLoaiKm", "MaLoaiKm", ctKhuyenMai.MaLoaiKm);
            ViewData["NhomSpkhuyemai"] = new SelectList(_context.Danhmucs, "MaDm", "MaDm", ctKhuyenMai.NhomSpkhuyemai);
            return View(ctKhuyenMai);
        }

        // GET: CTKhuyenMai/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CtKhuyenMais == null)
            {
                return NotFound();
            }

            var ctKhuyenMai = await _context.CtKhuyenMais
                .Include(c => c.MaLoaiKmNavigation)
                .Include(c => c.NhomSpkhuyemaiNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ctKhuyenMai == null)
            {
                return NotFound();
            }

            return View(ctKhuyenMai);
        }

        // POST: CTKhuyenMai/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CtKhuyenMais == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CtKhuyenMais'  is null.");
            }
            var ctKhuyenMai = await _context.CtKhuyenMais.FindAsync(id);
            if (ctKhuyenMai != null)
            {
                _context.CtKhuyenMais.Remove(ctKhuyenMai);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CtKhuyenMaiExists(int id)
        {
          return (_context.CtKhuyenMais?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
