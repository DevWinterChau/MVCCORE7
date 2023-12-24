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
using BAITAP.DTO;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Drawing.Printing;

namespace BAITAP.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class MhsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MhsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Sale")]

        // GET: Mhs
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false, string khuyenmai = "0")
        {
            // Start with the base query
            var query = _context.Mhs.Include(m => m.Danhmucs).AsQueryable();

            // Filter by category name if provided
            if (!string.IsNullOrEmpty(category) && category != "Tất cả")
            {
                query = query.Where(m => m.Danhmucs.Ten.Equals(category));
                ViewBag.category = category;
            }
            else
            {
                ViewBag.category = "Tất cả";
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Ten.Contains(keyword.Trim()) || x.Danhmucs.Ten.Contains(keyword.Trim()));
            }


            // Add more sorting options if needed

            // Apply pagination
            var items = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            // Tính toán các thông tin phân trang
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.keyword = keyword;
            ViewBag.Fill = Fill;

            // Populate the dropdown with categories
            ViewBag.ListDanhMuc = await _context.Danhmucs.ToListAsync();

            return View(items);


        }
        [Authorize(Roles = "Admin,Sale")]

        // GET: Mhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mh = await _context.Mhs
                .FirstOrDefaultAsync(m => m.Id == id);

            if (mh == null)
            {
                return NotFound();
            }

            var listPhienban = await _context.Mathangs
                .Include(m => m.Danhmucs)
                .Where(x => x.MaMhchinh.Equals(id))
                .ToListAsync();

            var detailmodel = new DetailMhs
            {
                MatHangChinh = mh,
                DanhSachPhienBan = listPhienban
            };

            return View(detailmodel);
        }
        [Authorize(Roles = "Admin")]

        // GET: Mhs/Create
        public async Task<IActionResult> Create()
        {
            var model = new PhienBansSanPhamDTO();
            ViewBag.MaDm = await _context.Danhmucs.ToListAsync();
            ViewBag.MaTH = await _context.Thuonghieus.ToListAsync();
            return View(model);
        }
        // POST: Mhs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
         [HttpPost]
         [ValidateAntiForgeryToken]
         public async Task<IActionResult> Create([Bind("Id,Ten,Hinhanh,Ngaytao,Ngaycapnhat")] Mh mh)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(mh);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             return View(mh);
         }*/
        [Authorize(Roles = "Admin")]

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] PhienBansSanPhamDTO mh, List<IFormFile> fileanh, IFormFile fileanhchinh)
        {
            try
            {
                var phienBansSanPhamDTO = JsonConvert.DeserializeObject<PhienBansSanPhamDTO>(mh.JsonData);

                // Process files
                if (fileanh != null && fileanh.Count > 0)
                {
                    int i = 0;
                    foreach (var file in fileanh)
                    {
                        var tenfile = DateTime.Now.ToString("yyyyMMddHHmmss") + file.FileName;
                        // Save the file using the fileanhFileName property
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", tenfile);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        if (phienBansSanPhamDTO.PhienBansanphamList.Count > 0)
                        {
                            phienBansSanPhamDTO.PhienBansanphamList[i].HinhAnh = tenfile;
                            i++;
                        }

                    }
                }
                if (fileanhchinh != null)
                {
                    var tenfile = DateTime.Now.ToString("yyyyMMddHHmmss") + fileanhchinh.FileName;
                    // Save the file using the fileanhFileName property
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", tenfile);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileanhchinh.CopyToAsync(stream);
                        phienBansSanPhamDTO.mathang.Hinhanh = tenfile;
                    }

                }
                if (mh != null)
                {
                    // Add the main product (mh.mathang)
                    _context.Mhs.Add(phienBansSanPhamDTO.mathang);
                    await _context.SaveChangesAsync();

                    // Add the variants (PhienBansanphamList)
                    foreach (var item in phienBansSanPhamDTO.PhienBansanphamList)
                    {

                        var newphienban = new Mathang
                        {
                            Ten = item.Ten,
                            GiaGoc = item.GiaGoc,
                            GiaBan = item.GiaBan,
                            SoLuong = item.SoLuong,
                            MoTa = item.MoTa,
                            HinhAnh = item.HinhAnh,
                            MaDm = item.MaDm,
                            Thuonghieu = item.Thuonghieu,
                            LuotMua = item.LuotMua,
                            LuotXem = item.LuotXem,
                            MaMhchinh = phienBansSanPhamDTO.mathang.Id,
                            Kichco = item.Kichco,
                            Chatlieu = item.Chatlieu,
                            Mausac = item.Mausac,
                            Donvitinh = item.Donvitinh,
                            Trangthai = item.Trangthai,
                            Iskhuyemmai = false,
                            Ngaytao = DateTime.Now,
                            Ngaycapnhat = DateTime.Now,
                        };
                        _context.Mathangs.Add(newphienban);
                    }
                    await _context.SaveChangesAsync();
                    return Json(new
                    {
                        success = true,
                        message = "Thêm các phiên bản sản phẩm thành công!",
                    });

                }

            }
            catch (Exception ex)
            {

            }
            return View(mh);
        }
        [Authorize(Roles = "Admin")]

        // GET: Mhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mhs == null)
            {
                return NotFound();
            }

            var mh = await _context.Mhs.FindAsync(id);
            if (mh == null)
            {
                return NotFound();
            }
            ViewData["MaDm"] = new SelectList(_context.Danhmucs, "MaDm", "Ten");
            return View(mh);
        }
        [Authorize(Roles = "Admin")]

        // POST: Mhs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ten,Hinhanh,Ngaytao,Ngaycapnhat, LuotXem, Madm, Mota")] Mh mh, IFormFile? fileanh)
        {
            if (id != mh.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (fileanh != null)
                {
                    var tenfile = DateTime.Now.ToString("yyyyMMddHHmmss") + fileanh.FileName;
                    // Save the file using the fileanhFileName property
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products", tenfile);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileanh.CopyToAsync(stream);
                        mh.Hinhanh = tenfile;
                    }

                }
                try
                {
                    _context.Update(mh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MhExists(mh.Id))
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
            return View(mh);
        }
        [Authorize(Roles = "Admin")]

        // GET: Mhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mhs == null)
            {
                return NotFound();
            }

            var mh = await _context.Mhs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mh == null)
            {
                return NotFound();
            }

            return View(mh);
        }
        [Authorize(Roles = "Admin")]

        // POST: Mhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mhs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Mhs'  is null.");
            }
            var mh = await _context.Mhs.FindAsync(id);
            if (mh != null)
            {
                _context.Mhs.Remove(mh);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin,Sale")]
        private bool MhExists(int id)
        {
            return (_context.Mhs?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
