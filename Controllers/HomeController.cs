using Microsoft.AspNetCore.Mvc;
using Pharmacy.Models;
using System.Diagnostics;
using System.Linq;

namespace Pharmacy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PharmacyContext _context;

        public HomeController(ILogger<HomeController> logger, PharmacyContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // Fetch products from the database
            var products = _context.Products.ToList();

            // Pass the products list to the Index view
            return View(products);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult Store()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
