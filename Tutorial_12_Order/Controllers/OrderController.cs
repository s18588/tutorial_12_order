using Microsoft.AspNetCore.Mvc;

namespace Tutorial_12_Order.Controllers
{
    public class OrderController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}