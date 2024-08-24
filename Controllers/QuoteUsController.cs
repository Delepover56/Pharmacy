using Microsoft.AspNetCore.Mvc;
using Pharmacy.Data;
using Pharmacy.Models;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
    public class QuoteUsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public QuoteUsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: QuoteUs
        public IActionResult Index()
        {
            return View();
        }

        // POST: QuoteUs
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Quotes quote)
        {
            if (ModelState.IsValid)
            {
                quote.DateSubmitted = DateTime.UtcNow; // Set the submission date
                _context.Quotes.Add(quote);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Your quote has been submitted successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(quote);
        }
    }
}
