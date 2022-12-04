using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OnlineSystem.Infrastructure;
using OnlineSystem.Infrastructure.DataBase.Models;
using OnlineSystem.Service.Interfaces;
using OnlineSystem.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnlineSystem.Service.Common;


namespace OnlineSystem.Service.Services
{
    public class ProductService : IProductService
    {
        private IOnlineShopContext _IOnlineShopContext { get; set; }
        private readonly IMapper _IMapper;


        public ProductService(IOnlineShopContext onlineShopContext  , IMapper IMapper)
        {

            _IOnlineShopContext = onlineShopContext;
            _IMapper = IMapper;
        }
        public     List<ProductViewModel> GetProductListMoc()
        {
            List<ProductViewModel> productList = new  List<ProductViewModel>();


            productList.Add(new ProductViewModel
            {
                ID = 1,
                CategoryID = 1,
                CategoryName = "TV",
                Description = "TV",
                HasAvailableStock = false,
               // Image = "Image\\Image.jpg",
                Name="Screen Samsung",
                Price=150,
            }
            );
            productList.Add(new ProductViewModel
            {
                ID = 2,
                CategoryID = 1,
                CategoryName = "TV",
                Description = "TV",
                HasAvailableStock = false,
               // Image = "Image\\Image.jpg",
                Name = "Screen Samsung",
                Price = 150,
            }
           );
            productList.Add(new ProductViewModel
            {
                ID = 3,
                CategoryID = 1,
                CategoryName = "TV",
                Description = "TV",
                HasAvailableStock = false,
               // Image = "Image\\Image.jpg",
                Name = "Screen Samsung",
                Price = 150,
            }
           );



            return productList;
        }

        List<ProductViewModel> IProductService.GetProductList(DataTableParameters dataTableAjaxPostModel , out int  TotalRecordsCount , out int FilteredRecordCount)
         {
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();



             #region Paramters



            string order =
                (dataTableAjaxPostModel.order != null && dataTableAjaxPostModel.order[0] != null && !string.IsNullOrEmpty(dataTableAjaxPostModel.order[0].column.ToString())
          ? dataTableAjaxPostModel.order[0].column.ToString() : null);


            string orderDir =
                (dataTableAjaxPostModel.order != null && dataTableAjaxPostModel.order[0] != null && !string.IsNullOrEmpty(dataTableAjaxPostModel.order[0].dir) ? dataTableAjaxPostModel.order[0].dir : null);


            int startRec = dataTableAjaxPostModel.start;
            int pageSize = dataTableAjaxPostModel.length;



            string Name = dataTableAjaxPostModel.columns[0].search.value;
            string Name_ar = dataTableAjaxPostModel.columns[1].search.value;
            string Description = dataTableAjaxPostModel.columns[2].search.value;
            string Price = dataTableAjaxPostModel.columns[3].search.value;


            string CategoryName = dataTableAjaxPostModel.columns[5].search.value;
            string ParentCategoryName = dataTableAjaxPostModel.columns[6].search.value;



            bool NameFound = !string.IsNullOrEmpty(Name);
            bool Name_ar_Found = !string.IsNullOrEmpty(Name_ar);
            bool DescriptionFound = !string.IsNullOrEmpty(Description);
            bool PriceFound = !string.IsNullOrEmpty(Price);

            bool CategoryNameFound = !string.IsNullOrEmpty(CategoryName);
            bool ParentCategoryNameFound = !string.IsNullOrEmpty(ParentCategoryName);

            #endregion

             IQueryable<Product> _ProductIQueryable = _IOnlineShopContext.Product.Include(s => s.Category).ThenInclude(s => s.ParentCategory);
             TotalRecordsCount = _ProductIQueryable.Count();
 
             #region Filters

            if (NameFound)
            {
                _ProductIQueryable = _ProductIQueryable.Where(x => x.Name != null && x.Name.ToLower().Trim().Contains(Name.ToLower().Trim()));
            }
            if (Name_ar_Found)
            {
                _ProductIQueryable = _ProductIQueryable.Where(x => x.Name_ar != null && x.Name_ar.ToLower().Trim().Contains(Name_ar.ToLower().Trim()));
            }
            if (PriceFound)
            {
                double priceValue;
                bool priceValueFound = double.TryParse(Price, out priceValue);


                if (priceValueFound)
                    _ProductIQueryable = _ProductIQueryable.Where(x => x.Price == priceValue);

            }


            if (DescriptionFound)
            {

                _ProductIQueryable = _ProductIQueryable.Where(x => x.Description != null && x.Description.ToLower().Trim().Contains(Description.ToLower().Trim()));
            }



            if (CategoryNameFound)
            {
                _ProductIQueryable = _ProductIQueryable.Where(x => x.Category != null && x.Category.Name != null

                && x.Category.Name.ToLower().Trim().Contains(CategoryName.ToLower().Trim()));
            }

            if (ParentCategoryNameFound)
            {

                _ProductIQueryable = _ProductIQueryable.Where(x => x.Category != null && x.Category.ParentCategory != null
                && x.Category.ParentCategory.Name != null
                && x.Category.ParentCategory.Name.ToLower().Trim().Contains(ParentCategoryName.ToLower().Trim()));
            }
            #endregion
             #region Sorting  

                 
            if (order != "7")
            {
                switch (order)
                {
                     case "0":
                             _ProductIQueryable = orderDir.Equals("DESC", StringComparison.CurrentCultureIgnoreCase) ? _ProductIQueryable.OrderByDescending(p => p.Name)
                            : _ProductIQueryable.OrderBy(p => p.Name);
                        break;

                 


                     default:
                        _ProductIQueryable = _ProductIQueryable.OrderByDescending(p => p.Name);
                        break;

                }
            }

            #endregion

            _ProductIQueryable= _ProductIQueryable.Skip(startRec).Take(pageSize);

            FilteredRecordCount = _ProductIQueryable.Count();


            var productDBList = _ProductIQueryable.ToList();
            productViewModels = _IMapper.Map<List<Product> , List<ProductViewModel>>(productDBList);
            return productViewModels;
        }

     public   List<CategoryViewModel> GetCategoryList(bool IsParentCategory)
        {
            List<CategoryViewModel> CategoryViewModelList = new List<CategoryViewModel>();
            var CategoryDBList = _IOnlineShopContext.Category.Where(s=>(IsParentCategory==true? s.ParentCategoryID==null:s.ParentCategoryID.HasValue&& s.ParentCategoryID.Value>1)).ToList();
            CategoryViewModelList = _IMapper.Map<List<Category>, List<CategoryViewModel>>(CategoryDBList);
            return CategoryViewModelList;
        }

        public ResultViewModel SaveProduct(ProductCreateViewModel productCreateViewModel)
        {
            ResultViewModel resultViewModel = new ResultViewModel();
            resultViewModel.Message = "Error";
            resultViewModel.Status = MessageTypeEnum.Error;

          var   product = new Product() 
            { 
             CategoryID= productCreateViewModel.CategoryID,
             Description= productCreateViewModel.Description,
             HasAvailableStock=productCreateViewModel.HasAvailableStock,
             Name=productCreateViewModel.Name,
             Name_ar=productCreateViewModel.Name_ar,
             Price = productCreateViewModel.Price,
             ProductImageName=productCreateViewModel.ProductImageName,
             ProductImagePath= productCreateViewModel.ProductImagePath,



         };
            _IOnlineShopContext.Product.Add(product);
            var rows = _IOnlineShopContext.SaveChanges();

            if (rows > 0)
            {
                resultViewModel.Message = "success";
                resultViewModel.Status = MessageTypeEnum.Success;
            }
           
            return resultViewModel;
        }


        public List<ProductStatus> GetProductStatusList() 
        {
            List<ProductStatus> productStatusList = new List<ProductStatus>();




            productStatusList.Add(new ProductStatus
            {
                StatusID=0,
                StatusName="Not in Stock",
            });

            productStatusList.Add(new ProductStatus
            {
                StatusID = 1,
                StatusName = "Available In Stock",
            });
            return productStatusList;


        }



    }
}
