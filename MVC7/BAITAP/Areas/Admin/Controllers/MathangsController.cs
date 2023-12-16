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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BAITAP.Areas.Admin.Controllers
{

    [Authorize]
    [Area("Admin")]
    public class MathangsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MathangsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Mathangs
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false, string khuyenmai = "0")
        {
            // Start with the base query
            var query = _context.Mathangs.Include(m => m.Danhmucs).AsQueryable();

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
            /*
            // Filter by khuyenmai
            if (khuyenmai == "1")
            {
                query = query.Where(m => m.CtkmNavigation.NgayKetThuc <= DateTime.Now && m.CtkmNavigation.NgayBatDau <= DateTime.Now);
                ViewBag.khuyenmai = "1";
            }
            else
            {
                ViewBag.khuyenmai = "0";
            }
            */
            // Filter by keyword if provided
            if (!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Ten.Contains(keyword.Trim()) || x.Danhmucs.Ten.Contains(keyword.Trim()) || x.MoTa.Contains(keyword.Trim()));
            }

            // Sort by price
            if (sort == "priceHighToLow")
            {
                query = query.OrderByDescending(m => m.GiaBan);
                ViewBag.sort = sort;
            }
            else if (sort == "priceLowToHigh")
            {
                query = query.OrderBy(m => m.GiaBan);
                ViewBag.sort = sort;
            }
            else
            {
                ViewBag.sort = "Không sắp xếp";
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

        // GET: Mathangs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Mathangs == null)
            {
                return NotFound();
            }

            var mathang = await _context.Mathangs
                .Include(m => m.Danhmucs)
                .FirstOrDefaultAsync(m => m.MaMh == id);
            if (mathang == null)
            {
                return NotFound();
            }

            return View(mathang);
        }

        // GET: Mathangs/Create
        public IActionResult Create()
        {
            ViewData["MaDm"] = new SelectList(_context.Danhmucs, "MaDm", "Ten");
            return View();
        }

        // POST: Mathangs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaMh,Ten,GiaGoc,GiaBan,SoLuong,MoTa,HinhAnh,MaDm,LuotXem,LuotMua")] Mathang mathang, IFormFile fileanh)
        {


            if (ModelState.IsValid)
            {
                if (fileanh != null && fileanh.Length > 0)
                {
                    // Tạo đường dẫn cho thư mục lưu trữ hình ảnh trong wwwroot/images/products
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");

                    // Tạo đường dẫn tới file hình ảnh
                    var fileName = Path.Combine(imagePath, fileanh.FileName);

                    // Lưu file xuống đường dẫn tạm thời
                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        await fileanh.CopyToAsync(stream);
                    }

                    // Cập nhật tên file hình ảnh trong đối tượng mathang
                    mathang.HinhAnh = fileanh.FileName;
                }
                // Set the MaDmNavigation property explicitly
                _context.Add(mathang);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If ModelState.IsValid is false, there are validation errors.
            // You can inspect ModelState.Errors to get more details.
            foreach (var value in ModelState.Values)
            {
                foreach (var error in value.Errors)
                {
                    // Log or inspect the error message
                    var errorMessage = error.ErrorMessage;
                }
            }

            // If there are validation errors, redisplay the form with error messages.
            ViewData["MaDm"] = new SelectList(_context.Danhmucs, "MaDm", "Ten", mathang.MaDm);
            return View(mathang);
        }

        // GET: Mathangs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Mathangs == null)
            {
                return NotFound();
            }

            var mathang = await _context.Mathangs.FindAsync(id);
            if (mathang == null)
            {
                return NotFound();
            }
            ViewData["MaDm"] = new SelectList(_context.Danhmucs, "MaDm", "Ten", mathang.MaDm);
            ViewData["MaMHS"] = new SelectList(_context.Mhs, "Id", "Ten", mathang.MaMhchinh);
            return View(mathang);
        }

        // POST: Mathangs/Edit/5 
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaMh,Ten,GiaGoc,GiaBan,SoLuong,MoTa,HinhAnh,MaDm,LuotXem,LuotMua, Mausac, Chatlieu, Kichthuoc, Trangthai, MaMh, Thuonghieu, Donvitinh, Kichco, MaMhchinh")] Mathang mathang, IFormFile? fileanh, bool Trangthai)
        {
            if (id != mathang.MaMh)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (fileanh != null && fileanh.Length > 0)
                {
                    // Tạo đường dẫn cho thư mục lưu trữ hình ảnh trong wwwroot/images/products
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "products");

                    var tenfile = DateTime.Now.ToString("yyyyMMddHHmmss") + fileanh.FileName;

                    // Tạo đường dẫn tới file hình ảnh
                    var fileName = Path.Combine(imagePath, tenfile);

                    // Lưu file xuống đường dẫn tạm thời
                    using (var stream = new FileStream(fileName, FileMode.Create))
                    {
                        await fileanh.CopyToAsync(stream);
                    }

                    // Cập nhật tên file hình ảnh trong đối tượng mathang
                    mathang.HinhAnh = tenfile;
                }
                try
                {
                    if (Trangthai) mathang.Trangthai = true; else mathang.Trangthai = false;
                    mathang.Ngaycapnhat = DateTime.Now;
                    _context.Update(mathang);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MathangExists(mathang.MaMh))
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
            else
            {
                // Dữ liệu không hợp lệ, xử lý lỗi
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    // Xử lý mỗi lỗi, ví dụ: Log hoặc thêm vào một danh sách lỗi để hiển thị cho người dùng
                    Console.WriteLine(error.ErrorMessage);
                }

            }
            ViewData["MaDm"] = new SelectList(_context.Danhmucs, "MaDm", "Ten", mathang.MaDm);
            ViewData["MaMHS"] = new SelectList(_context.Mhs, "Id", "Ten", mathang.MaMhchinh);
            return View(mathang);
        }

        // GET: Mathangs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Mathangs == null)
            {
                return NotFound();
            }

            var mathang = await _context.Mathangs
                .Include(m => m.Danhmucs)
                .FirstOrDefaultAsync(m => m.MaMh == id);
            if (mathang == null)
            {
                return NotFound();
            }

            return View(mathang);
        }

        // POST: Mathangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Mathangs == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Mathangs'  is null.");
            }
            var mathang = await _context.Mathangs.FindAsync(id);
            if (mathang != null)
            {
                _context.Mathangs.Remove(mathang);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MathangExists(int id)
        {
            return (_context.Mathangs?.Any(e => e.MaMh == id)).GetValueOrDefault();
        }
    }
}
