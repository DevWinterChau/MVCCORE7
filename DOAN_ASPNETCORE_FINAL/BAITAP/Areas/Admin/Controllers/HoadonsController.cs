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
using BAITAP.DTO;

namespace BAITAP.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class HoadonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HoadonsController(ApplicationDbContext context)
        {
            _context = context;
        }
        [Authorize(Roles = "Admin,Sale")]
        // GET: Hoadons
        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false, DateTime? ngaybatdau = null, DateTime? ngayketthuc = null, int Trangthai = 5)
        {
            
            var applicationDbContext = await _context.Hoadons.Include(c => c.MakhNavigation).Include(c => c.Cthoadons).ToListAsync();
            var totalItems = applicationDbContext.Count();

            if (Trangthai != 5)
                applicationDbContext = applicationDbContext.Where(x => x.Trangthai.Equals(Trangthai)).ToList();
            // Filter by keyword if provided
            if (!string.IsNullOrEmpty(keyword))
            {
                applicationDbContext = applicationDbContext.Where(x => x.MakhNavigation.Ten.Contains(keyword.Trim())
                                                           || x.Tennguoinhan.Contains(keyword.Trim())
                                                           || x.Sodienthoai.Contains(keyword.Trim())
                                                            || x.Diachi.Contains(keyword.Trim())
                                                             || x.Tinh.Contains(keyword.Trim()) 
                                                              || x.Xaphuong.Contains(keyword.Trim())
                                                               || x.Quanhuyen.Contains(keyword.Trim())).ToList();
            }
            if (!string.IsNullOrEmpty(ngaybatdau.ToString()) && !string.IsNullOrEmpty(ngayketthuc.ToString()))
            {
                applicationDbContext = applicationDbContext.Where(x => x.Ngay >= ngaybatdau
                                                          && x.Ngay <= ngayketthuc).ToList();
            }

            // Apply pagination
            var items = applicationDbContext.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            // Tính toán các thông tin phân trang
            var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.keyword = keyword;
            ViewBag.Fill = Fill;
            ViewBag.Trangthai = Trangthai;
            // Populate the dropdown with categories
            ViewBag.ListDanhMuc = await _context.LoaiKhuyenMais.ToListAsync();
            return View(items);
        }
        [Authorize(Roles = "Admin,Sale")]
        // GET: Hoadons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Hoadons == null)
            {
                return NotFound();
            }

            var hoadon = await _context.Hoadons
                .Include(h => h.MakhNavigation)
                .Include(x => x.Cthoadons)
                .ThenInclude(m => m.MamhNavigation)
                .FirstOrDefaultAsync(m => m.Mahd == id);
            if (hoadon == null)
            {
                return NotFound();
            }

            return View(hoadon);
        }
        public class OrderData
        {
            public Hoadon Hoadon { get; set; }
            public List<ChiTietSanPhamHoaDon> ListSanPham { get; set; }
        }
        // GET: Hoadons/Create
        [Authorize(Roles = "Admin,Sale")]

        public async Task<IActionResult> Create()
        {
            DateTime currentDate = DateTime.Now;

            var query = from mathang in _context.Mathangs
                        join ctkm in _context.CtKhuyenMaiSanPhams
                        on mathang.MaMh equals ctkm.Mamh into ctkmGroup
                        from ctkm in ctkmGroup.DefaultIfEmpty()
                        join danhmuc in _context.Danhmucs
                        on mathang.MaDm equals danhmuc.MaDm
                        //where ctkm.MaCtkmNavigation.NgayBatDau <= currentDate && ctkm.MaCtkmNavigation.NgayKetThuc <= currentDate
                        select new SanphamKhuyemMai
                        {
                            mathang = mathang,
                            danhmuc = danhmuc,
                            ctkm = ctkm,
                            Phantramkhuyenmai = ctkm != null ? ctkm.Phantramkhuyenmai != null ? ctkm.Phantramkhuyenmai : 0 : 0,
                            Giakhuyemai = ctkm != null ? mathang.GiaBan - mathang.GiaBan * (ctkm.Phantramkhuyenmai / 100.0) : 0
                        };
            ViewBag.DSSP = await query.ToListAsync();
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "Ten");
            return View();
        }
        [Authorize(Roles = "Admin,Sale")]

        // POST: Hoadons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] OrderData orderData)
        {

            _context.Add(orderData.Hoadon);
            await _context.SaveChangesAsync();
            int tongtien = 0;
            foreach( var item in orderData.ListSanPham)
            {
                var cthd = new Cthoadon();
                cthd.Mahd = orderData.Hoadon.Mahd;
                cthd.Mamh = item.MaSP;
                cthd.Soluong = item.Soluong;
                cthd.Dongia = item.GiaKhuyenMai > 0 ? item.GiaKhuyenMai : item.GiaBan;
                cthd.Thanhtien = cthd.Soluong * cthd.Dongia;
                tongtien += cthd.Thanhtien;
                _context.Add(cthd);
            }
            orderData.Hoadon.Tongtien = tongtien;


            _context.Hoadons.Update(orderData.Hoadon);
            await _context.SaveChangesAsync();
            DateTime currentDate = DateTime.Now;

            var query = from mathang in _context.Mathangs
                        join ctkm in _context.CtKhuyenMaiSanPhams
                        on mathang.MaMh equals ctkm.Mamh into ctkmGroup
                        from ctkm in ctkmGroup.DefaultIfEmpty()
                        join danhmuc in _context.Danhmucs
                        on mathang.MaDm equals danhmuc.MaDm
                        //where ctkm.MaCtkmNavigation.NgayBatDau <= currentDate && ctkm.MaCtkmNavigation.NgayKetThuc <= currentDate
                        select new SanphamKhuyemMai
                        {
                            mathang = mathang,
                            danhmuc = danhmuc,
                            ctkm = ctkm,
                            Phantramkhuyenmai = ctkm != null ? ctkm.Phantramkhuyenmai != null ? ctkm.Phantramkhuyenmai : 0 : 0,
                            Giakhuyemai = ctkm != null ? mathang.GiaBan - mathang.GiaBan * (ctkm.Phantramkhuyenmai / 100.0) : 0
                        };
            ViewBag.DSSP = await query.ToListAsync();
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "Ten", orderData.Hoadon.Makh);
            return Json(new { 
                success = true ,
                message = "Thêm thành công"
            });

        }
        [Authorize(Roles = "Admin,Sale")]

        // GET: Hoadons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Hoadons == null)
            {
                return NotFound();
            }

            var hoadon = await _context.Hoadons.FindAsync(id);
            if (hoadon == null)
            {
                return NotFound();
            }
            DateTime currentDate = DateTime.Now;

            var query = from mathang in _context.Mathangs
                        join ctkm in _context.CtKhuyenMaiSanPhams
                        on mathang.MaMh equals ctkm.Mamh into ctkmGroup
                        from ctkm in ctkmGroup.DefaultIfEmpty()
                        join danhmuc in _context.Danhmucs
                        on mathang.MaDm equals danhmuc.MaDm
                        //where ctkm.MaCtkmNavigation.NgayBatDau <= currentDate && ctkm.MaCtkmNavigation.NgayKetThuc <= currentDate
                        select new SanphamKhuyemMai
                        {
                            mathang = mathang,
                            danhmuc = danhmuc,
                            ctkm = ctkm,
                            Phantramkhuyenmai = ctkm != null ? ctkm.Phantramkhuyenmai != null ? ctkm.Phantramkhuyenmai : 0 : 0,
                            Giakhuyemai = ctkm != null ? mathang.GiaBan - mathang.GiaBan * (ctkm.Phantramkhuyenmai / 100.0) : 0
                        };
            ViewBag.DSSP = await query.ToListAsync();
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "Ten", hoadon.Makh);
            return View(hoadon);
        }
        [Authorize(Roles = "Admin,Sale")]

        // POST: Hoadons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Mahd,Makh,Ngay,Tongtien,Trangthai, Tennguoinhan, Sodienthoai, Diachi, Xaphuong, Quanhuyen, Tinh")] Hoadon hoadon)
        {
            if (id != hoadon.Mahd)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hoadon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HoadonExists(hoadon.Mahd))
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
            ViewData["Makh"] = new SelectList(_context.Khachhangs, "MaKh", "Ten", hoadon.Makh);
            return View(hoadon);
        }
        [Authorize(Roles = "Admin")]

        // GET: Hoadons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Hoadons == null)
            {
                return NotFound();
            }

            var hoadon = await _context.Hoadons
                .Include(h => h.MakhNavigation)
                .FirstOrDefaultAsync(m => m.Mahd == id);
            if (hoadon == null)
            {
                return NotFound();
            }

            return View(hoadon);
        }
        [Authorize(Roles = "Admin")]

        // POST: Hoadons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Hoadons == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Hoadons'  is null.");
            }
            var hoadon = await _context.Hoadons.FindAsync(id);
            if (hoadon != null)
            {
                _context.Hoadons.Remove(hoadon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HoadonExists(int id)
        {
            return (_context.Hoadons?.Any(e => e.Mahd == id)).GetValueOrDefault();
        }
    }
}
