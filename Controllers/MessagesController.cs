using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pharmacy.Data; // Import the namespace where ApplicationDbContext is located
using Pharmacy.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
    public class MessagesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MessagesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "admin")]
        public IActionResult Index()
        {
            // Fetch all contact form messages from the database
            var messages = _context.ContactForms.ToList();
            return View(messages);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            // Find the message by its ID
            var message = await _context.ContactForms.FindAsync(id);
            if (message == null)
            {
                return NotFound();
            }

            // Remove the message from the database
            _context.ContactForms.Remove(message);
            await _context.SaveChangesAsync();

            // Redirect back to the index view
            return RedirectToAction("Index");
        }
    }
}
