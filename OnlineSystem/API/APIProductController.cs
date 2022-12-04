using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineSystem.Controllers;
using OnlineSystem.Service.Interfaces;
using OnlineSystem.Service.ViewModels;



namespace OnlineSystem.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIProductController : Controller
    {


        private IProductService _IProductService { get; set; }
        private readonly ILogger<ProductController> _ILogger;

        public APIProductController(IProductService IProductService, ILogger<ProductController> iLogger)
        {
            _IProductService = IProductService;
            _ILogger = iLogger;
        }


        [HttpGet]
        public IActionResult GetProductModelList()
        {
            try
            {
                List<ProductViewModel> ProductStatusList = new List<ProductViewModel>();
                ProductStatusList = _IProductService.GetAllProductList();
                return Json(ProductStatusList, new System.Text.Json.JsonSerializerOptions());
            }
            catch (Exception ex)
            {
                _ILogger.LogError(ex, "error in GetProductModelList");
                return Json(new List<ProductViewModel>(), new System.Text.Json.JsonSerializerOptions());


            }
        }

    }
}
