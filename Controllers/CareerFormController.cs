using Microsoft.AspNetCore.Mvc;
using Pharmacy.Data;
using Pharmacy.Models;
using System;
using System.Threading.Tasks;

namespace Pharmacy.Controllers
{
    public class CareerFormController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CareerFormController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CareerForm/Create
        public IActionResult Index()
        {
            return View();
        }

        // POST: CareerForm/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FullName,Email,PhoneNumber,Education,DateOfApplication")] CareerForm careerForm)
        {
            careerForm.Status = false;
            if (ModelState.IsValid)
            {
                careerForm.DateOfApplication = DateTime.Now; // Set the current date if not provided
                _context.Add(careerForm);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Your application has been submitted successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(careerForm);
        }
    }
}
