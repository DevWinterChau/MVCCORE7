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
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BAITAP.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ChitietKhuyenMaiSanPhamController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChitietKhuyenMaiSanPhamController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ChitietKhuyenMaiSanPham
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false)
        {
            var applicationDbContext = await _context.CtKhuyenMaiSanPhams.Include(c => c.MaCtkmNavigation).Include(c => c.MamhNavigation).ToListAsync();
            var totalItems = applicationDbContext.Count() ;

            // Filter by category name if provided
            if (!string.IsNullOrEmpty(category) && category != "Tất cả")
            {
                applicationDbContext =  applicationDbContext.Where(m => m.MaCtkmNavigation.TenKm.Equals(category)).ToList();
                ViewBag.category = category;
            }
            else
            {
                ViewBag.category = "Tất cả";
            }


            // Filter by keyword if provided
            if (!string.IsNullOrEmpty(keyword))
            {
                applicationDbContext = applicationDbContext.Where(x => x.MaCtkmNavigation.TenKm.Contains(keyword.Trim())
                                                           || x.MamhNavigation.Ten.Contains(keyword.Trim())).ToList();
            }
            // Apply pagination
            var items =  applicationDbContext.Skip((page - 1) * pageSize).Take(pageSize).ToList();

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
            ViewData["MaCtkm"] = new SelectList(_context.CtKhuyenMais, "Id", "TenKm");
            ViewData["Mamh"] = new SelectList(_context.Mathangs, "MaMh", "Ten");
            return View();
        }

        // POST: ChitietKhuyenMaiSanPham/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MaCtkm,Mamh,Phantramkhuyenmai")] CtKhuyenMaiSanPham ctKhuyenMaiSanPham, string SelectedProducts)
        {
            if (ModelState.IsValid)
            {
                List<string> productIDList = JsonConvert.DeserializeObject<List<string>>(SelectedProducts);

                foreach (var item in productIDList)
                {
                    if (item != null)
                    {
                        // Create a new instance for each selected product
                        var newCtKhuyenMaiSanPham = new CtKhuyenMaiSanPham
                        {
                            MaCtkm = ctKhuyenMaiSanPham.MaCtkm,
                            Mamh = Convert.ToInt32( item),
                            Phantramkhuyenmai = ctKhuyenMaiSanPham.Phantramkhuyenmai
                        };

                        _context.Add(newCtKhuyenMaiSanPham);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MaCtkm"] = new SelectList(_context.CtKhuyenMais, "Id", "TenKm", ctKhuyenMaiSanPham.MaCtkm);
            ViewData["Mamh"] = new SelectList(_context.Mathangs, "MaMh", "Ten", ctKhuyenMaiSanPham.Mamh);
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
            ViewData["MaCtkm"] = new SelectList(_context.CtKhuyenMais, "Id", "TenKm", ctKhuyenMaiSanPham.MaCtkm);
            ViewData["Mamh"] = new SelectList(_context.Mathangs, "MaMh", "Ten", ctKhuyenMaiSanPham.Mamh);
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
            ViewData["MaCtkm"] = new SelectList(_context.CtKhuyenMais, "Id", "TenKm", ctKhuyenMaiSanPham.MaCtkm);
            ViewData["Mamh"] = new SelectList(_context.Mathangs, "MaMh", "Ten", ctKhuyenMaiSanPham.Mamh);
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
                .FirstOrDefaultAsync(m => m.Id == id);
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
