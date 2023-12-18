using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;

namespace BAITAP.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public  RolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index(int page = 1, int pageSize = 8, string keyword = null, string category = null, string sort = null, bool Fill = false)
        {
            var applicationDbContext = await _roleManager.Roles.ToListAsync();
            var totalItems = applicationDbContext.Count();
            // Filter by keyword if provided
            if (!string.IsNullOrEmpty(keyword))
            {
                applicationDbContext = applicationDbContext.Where(x => x.Name.Contains(keyword.Trim())).ToList();
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

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            if(!_roleManager.RoleExistsAsync(role.Name).GetAwaiter().GetResult() ) {
                _roleManager.CreateAsync(new IdentityRole(role.Name)).GetAwaiter().GetResult();
            }
            return RedirectToAction("Index");
        }
    }
}
