using Microsoft.AspNetCore.Mvc;
using Pharmacy.Data;
using Pharmacy.Models;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SubmitContactForm(ContactForm model)
        {
            if (ModelState.IsValid)
            {
                _context.ContactForms.Add(model);
                await _context.SaveChangesAsync();
                TempData["Message"] = "Your message has been sent.";
                return RedirectToAction("Index");
            }

            return View("Index", model);
        }
    }
}
