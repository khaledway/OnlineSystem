using Microsoft.AspNetCore.Mvc;
using OnlineSystem.Service.Interfaces;

namespace OnlineSystem.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _IProductService { get; set; }
        public ProductController(IProductService IProductService)
        {
            _IProductService = IProductService;

        }
        public IActionResult Index()
        {
            var product = _IProductService.GetProductList();
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        
    }
}
