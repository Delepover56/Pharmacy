using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pharmacy.Data;
using Pharmacy.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
    public class UserQuotesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserQuotesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: UserQuotes
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Index()
        {
            try
            {
                var quotes = await _context.Quotes.ToListAsync();
                return View(quotes);
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }

        // POST: UserQuotes/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var quote = await _context.Quotes.FindAsync(id);
                if (quote == null)
                {
                    return NotFound();
                }

                _context.Quotes.Remove(quote);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View("Error");
            }
        }
    }
}
