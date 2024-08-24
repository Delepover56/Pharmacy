using Microsoft.AspNetCore.Mvc;
using Pharmacy.Data;
using Pharmacy.Models;
using System.Linq;

namespace Pharmacy.Controllers
{
    public class StoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult List()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult GetProductDetails(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return Json(new
            {
                imagePath = product.ImagePath,
                prodName = product.ProdName,
                description = product.Description,
                price = product.Price,
                stock = product.Stock
            });
        }
    }
}
