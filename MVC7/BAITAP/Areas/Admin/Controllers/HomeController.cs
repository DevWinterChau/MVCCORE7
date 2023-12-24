using BAITAP.Areas.Admin.DTO;
using BAITAP.Data;
using BAITAP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

namespace BAITAP.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Sale")]
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;


        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index(DateTime? starttime = null , DateTime? endtime =null)
        {
            int startYear = starttime.HasValue ? starttime.Value.Year : DateTime.Now.Year;
            int endYear = endtime.HasValue ? endtime.Value.Year : DateTime.Now.Year;
            
            var SpBanChay = await _context.Hoadons
                .Include(x => x.Cthoadons)
                .ThenInclude(mh => mh.MamhNavigation)
                .Where(hd => (!starttime.HasValue || hd.Ngay.Value.Year >= startYear) && (!endtime.HasValue || hd.Ngay.Value.Year <= endYear))
                .ToListAsync();
            List<DataPoint> dataPoints = new List<DataPoint>();
            
            
            var doanhThuTheoThang = await _context.Hoadons
                  .Where(hd => (!starttime.HasValue || hd.Ngay.Value.Year >= startYear) && (!endtime.HasValue || hd.Ngay.Value.Year <= endYear))
                  .GroupBy(hd => hd.Ngay.Value.Month)
                  .Select(gr => new
                  {
                      Thang = gr.Key,
                      TongDoanhThu = gr.Sum(hd => hd.Tongtien)
                  })
                  .OrderBy(item => item.Thang)
                  .ToListAsync();


            List<DataPoint> dataPointdoanhthu = new List<DataPoint>();
            foreach (var dt in doanhThuTheoThang)
            {
                dataPointdoanhthu.Add(new DataPoint(dt.Thang.ToString(),(double) dt.TongDoanhThu));
            }

            foreach (var s in SpBanChay.Take(10))
            {
                foreach(var p in s.Cthoadons) {
                    var dataacheck = dataPoints.FirstOrDefault(x => x.Label == p.MamhNavigation.Ten);
                    if (dataacheck != null)
                    {
                        dataacheck.Label = p.MamhNavigation.Ten;
                        dataacheck.Y +=(double) p.Soluong;
                    }
                    else
                    {
                        dataPoints.Add(new DataPoint(p.MamhNavigation.Ten,(double) p.Soluong));
                    }

                }
            }
            if(starttime == null || (endtime == null))
            {
                    var timenow = DateTime.Now;
                    int currentYear = timenow.Year;
                    ViewBag.Time = $"{currentYear}";
                    ViewBag.Nam = currentYear;
            }
            else
            {
                // Extract months from start and end time
                int startMonth = starttime.Value.Month;
                int endMonth = endtime.Value.Month;
                // Extract years from start and end time
                if(startYear  == endYear)
                {
                    // Set ViewBag.Time based on the months
                    ViewBag.Time = $"Tháng {startMonth} đến tháng {endMonth} {startYear}";
                }
                else
                {
                    ViewBag.Time = $"Tháng {startMonth}/{startYear} đến tháng {endMonth}/{endYear}";
                    ViewBag.Nam = $"{startYear} - {endYear}";

                }
            }
            var queryKH =  _context.Khachhangs.AsQueryable();
            ViewData["kh"] = await queryKH.CountAsync();
            var queryNV = _context.Nhanviens.AsQueryable();
            ViewData["nv"] = await queryNV.CountAsync();
            ViewBag.DataPointsDoanhthu = JsonConvert.SerializeObject(dataPointdoanhthu);
            ViewBag.DataPoints = JsonConvert.SerializeObject(dataPoints);
            return View();
        }

        public IActionResult Info()
        {
            return View();
        }
        public IActionResult setting()
        {
            return View();
        }
        public IActionResult changepass()
        {
            return View();
        }

    }
}