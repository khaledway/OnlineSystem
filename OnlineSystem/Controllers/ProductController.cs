using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OnlineSystem.Service.Interfaces;
using OnlineSystem.Service.ViewModels;
using static OnlineSystem.Service.Common;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json.Linq;

namespace OnlineSystem.Controllers
{
    public class ProductController : Controller
    {
        private IProductService _IProductService { get; set; }
        private readonly ILogger<ProductController> _ILogger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IConfiguration _configuration;

        public ProductController(IProductService IProductService, ILogger<ProductController> iLogger, IWebHostEnvironment webHostEnvironment, IConfiguration configuration)
        {
            _IProductService = IProductService;
            _ILogger = iLogger;
            _webHostEnvironment = webHostEnvironment;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        { 
            return View();
        }

        #region Actions and function


        [HttpPost]
        public ActionResult SaveProducData([FromForm] ProductCreateViewModel ProductCreateViewModel)
        {

            ResultViewModel resultViewModel = new ResultViewModel();
            resultViewModel.Message = "Error";
            resultViewModel.Status = MessageTypeEnum.Error;

          
            try
            {

                if (!ModelState.IsValid)
                {


                    string errors = JsonConvert.SerializeObject(ModelState.Values
        .SelectMany(state => state.Errors)
        .Select(error => error.ErrorMessage));

                    resultViewModel.Message = errors;

                    return Json(resultViewModel, new System.Text.Json.JsonSerializerOptions());
                }



                bool newIcon = ProductCreateViewModel.ImageFile != null && ProductCreateViewModel.ImageFile.Length > 0;
                bool iconSaved = false;
                if (newIcon)
                {
                    string directoryPath = "/Product";
                    iconSaved = SaveFile(ProductCreateViewModel.ImageFile, directoryPath, DateTime.Now, out string filePathUrl);
                    if (iconSaved)
                    {
                        ProductCreateViewModel.ProductImageName = ProductCreateViewModel.ImageFile.FileName;
                        ProductCreateViewModel.ProductImagePath = filePathUrl;
                    }
                    else
                    {
                        resultViewModel.Message = "Upload Error";
                    }
                }


                if (iconSaved)
                {
                    resultViewModel = _IProductService.SaveProduct(ProductCreateViewModel);
                }
                else
                {
                    
                }
                    return Json(resultViewModel, new System.Text.Json.JsonSerializerOptions());

            }
            catch (Exception ex)
            {
                _ILogger.LogError(ex, "Exception in SaveProducData");
                resultViewModel.Message = "error in server";
                resultViewModel.Status = MessageTypeEnum.Exception;
                resultViewModel.ConsoleMessage = ex.GetBaseException().Message;
                return Json(resultViewModel, new System.Text.Json.JsonSerializerOptions());
            }   
        
        }






        private bool SaveFile(IFormFile file, string directoryPath, DateTime now, out string filePathUrl)
        {
            bool saved = false;
            filePathUrl = "";
            try
            {
                if (file != null && file.Length > 0)
                {
                    string webRootPath = _webHostEnvironment.ContentRootPath;
                    string uploadDir = "";
       
                    uploadDir = Path.Combine(GetConfigurationSection("SharedStoragePath").Value + directoryPath);
                    var originalFilename = Path.GetFileName(file.FileName);
                    var fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c, '_'));
                    string newFileName = fileName + now.ToString("_yyyyMMdd_HHmmss") + Path.GetExtension(file.FileName);
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);
                    var filePath = Path.Combine(uploadDir, newFileName);
                    filePathUrl = "/UploadedFiles" + directoryPath + "/" + newFileName;

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                        stream.Flush();
                        saved = true;
                        return saved;
                    }
                }
            }
            catch (Exception ex)
            {
                _ILogger.LogError(ex.Message, ex, false);
            }
            return saved;
        }
        private IConfigurationSection GetConfigurationSection(string key)
        {
            return this._configuration.GetSection(key);
        }



        [HttpGet]
        public IActionResult GetProductStatus()
        {
            try
            {
                List<ProductStatus> ProductStatusList = new List<ProductStatus>();
                ProductStatusList = _IProductService.GetProductStatusList();
                return Json(ProductStatusList, new System.Text.Json.JsonSerializerOptions());
            }
            catch (Exception ex)
            {
                _ILogger.LogError(ex, "error in ProductStatusList");
                return Json(new List<CategoryViewModel>(), new System.Text.Json.JsonSerializerOptions());


            }
        }

        [HttpGet]
        public IActionResult GetCategoryList(bool IsParentCategory=false)
        {
            try
            {
                List<CategoryViewModel> CategoryViewModelList = new List<CategoryViewModel>();
                CategoryViewModelList = _IProductService.GetCategoryList(IsParentCategory);
                return Json(CategoryViewModelList, new System.Text.Json.JsonSerializerOptions());
            }
            catch (Exception ex)
            {
                _ILogger.LogError(ex, "error in GetCategoryList");
                return Json(new List<CategoryViewModel>(), new System.Text.Json.JsonSerializerOptions());


            }
        }





        [HttpPost]

        public IActionResult GetProductList([FromBody] DataTableParameters dataTableAjaxPostModel)
        {
            try

            {

                int TotalRecordsCount = 0;
                int FilteredRecordCount = 0;


                string draw = dataTableAjaxPostModel.draw.ToString();

                
                List<ProductViewModel> ProductViewModelList = _IProductService.GetProductList(dataTableAjaxPostModel , out  TotalRecordsCount, out  FilteredRecordCount);


                return this.Json(new
                {
                    draw = Convert.ToInt32(draw),
                    recordsTotal = TotalRecordsCount,
                    recordsFiltered = TotalRecordsCount,
                    data = ProductViewModelList
                });


            }

            catch ( Exception ex)
            {


                _ILogger.LogError(ex.GetBaseException().Message, ex, false);

                return this.Json(new
                {
                    draw = 0,
                    recordsTotal = 0,
                    recordsFiltered = 0,
                    data = new List<ProductViewModel>(),
                }, new System.Text.Json.JsonSerializerOptions());

            }
            
            }



                #endregion
            }
}
