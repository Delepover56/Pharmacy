using Microsoft.AspNetCore.Mvc;

namespace Pharmacy.Controllers
{
    public class CareerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
