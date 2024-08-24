using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
    [Authorize(Roles = "admin")]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DashboardController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            // Fetching the counts for different roles
            var totalAdmins = (await _userManager.GetUsersInRoleAsync("admin")).Count;
            var totalUsers = (await _userManager.GetUsersInRoleAsync("user")).Count;
            var totalCandidates = (await _userManager.GetUsersInRoleAsync("candidate")).Count;

            // Fetching counts for other entities
            var totalProducts = _context.Products.Count();
            var totalMessages = _context.ContactForms.Count();
            var totalQuotes = _context.Quotes.Count();

            // Passing data to the view using ViewData
            ViewData["TotalAdmins"] = totalAdmins;
            ViewData["TotalUsers"] = totalUsers;
            ViewData["TotalCandidates"] = totalCandidates;
            ViewData["TotalProducts"] = totalProducts;
            ViewData["TotalMessages"] = totalMessages;
            ViewData["TotalQuotes"] = totalQuotes;

            return View();
        }
    }
}
