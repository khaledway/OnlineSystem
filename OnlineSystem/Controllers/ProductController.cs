using Microsoft.AspNetCore.Mvc;

namespace OnlineSystem.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        
    }
}
