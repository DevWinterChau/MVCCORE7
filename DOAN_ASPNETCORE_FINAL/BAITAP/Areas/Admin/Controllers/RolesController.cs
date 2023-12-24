using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Authorization;

namespace BAITAP.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
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

        [HttpGet]
        public IActionResult Edit(string id)
        {
            if(id == "")
            {
                return NotFound();
            }
            var role = _roleManager.Roles.FirstOrDefault(x => x.Id.Equals(id));
            return View(role);
        }
        [HttpPost]
        public IActionResult Edit(IdentityRole role)
        {
            // Find the role by ID
            var existingRole = _roleManager.FindByIdAsync(role.Id).GetAwaiter().GetResult();

            // Check if the role exists
            if (existingRole != null)
            {
                // Update the properties of the existing role
                existingRole.Name = role.Name; // Update other properties as needed

                // Save the changes to the database
                var updateResult = _roleManager.UpdateAsync(existingRole).GetAwaiter().GetResult();

                if (updateResult.Succeeded)
                {
                    // Redirect to the Index action if the update is successful
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle the case where the update failed (e.g., validation errors)
                    foreach (var error in updateResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }

                    // Return the view with errors
                    return View(role);
                }
            }
            else
            {
                // Handle the case where the role with the given ID was not found
                ModelState.AddModelError(string.Empty, "Role not found");
                return View(role);
            }
        }
    }
}
