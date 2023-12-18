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

    [Authorize]
    [Area("Admin")]
    public class HoadonsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HoadonsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Hoadons
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Hoadons.Include(h => h.MakhNavigation);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Hoadons/Details/5
        public async Task<IActionResult> Details(int? id)
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
        public class OrderData
        {
            public Hoadon Hoadon { get; set; }
            public List<ChiTietSanPhamHoaDon> ListSanPham { get; set; }
        }
        // GET: Hoadons/Create
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
