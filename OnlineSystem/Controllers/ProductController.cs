using Microsoft.AspNetCore.Mvc;
using OnlineSystem.Service.Interfaces;

namespace OnlineSystem.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _IProductService { get; set; }

       
        private readonly ILogger<ProductController> _ILogger;

        public ProductController(IProductService IProductService, ILogger<ProductController> iLogger)
        {
            _IProductService = IProductService;
            _ILogger = iLogger;
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
