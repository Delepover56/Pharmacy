using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Data;
using Pharmacy.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("Dashboard/[controller]/[action]")]
    public class CandidatesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public CandidatesController(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var candidates = _context.CareerForms.ToList();
            return View(candidates);
        }

        [HttpGet]
        public IActionResult Approve(int id)
        {
            var form = _context.CareerForms.Find(id);
            if (form == null)
            {
                return NotFound();
            }
            return View(form);
        }

        [HttpPost]
        public async Task<IActionResult> Approve (CareerForm career)
        {
            career.Status = true;
            var user = _userManager.FindByEmailAsync(career.Email).Result;

            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, "user");
                await _userManager.AddToRoleAsync(user, "candidate");
            }

            if (ModelState.IsValid)
            {
                _context.CareerForms.Remove(career);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(career);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var candidateForm = _context.CareerForms.FirstOrDefault(c => c.Id == id);
            if (candidateForm == null)
            {
                return NotFound();
            }

            // Remove the application form
            _context.CareerForms.Remove(candidateForm);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
