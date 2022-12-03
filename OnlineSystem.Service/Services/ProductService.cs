using AutoMapper;
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
                Image = "Image\\Image.jpg",
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
                Image = "Image\\Image.jpg",
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
                Image = "Image\\Image.jpg",
                Name = "Screen Samsung",
                Price = 150,
            }
           );



            return productList;
        }

        List<ProductViewModel> IProductService.GetProductList()
         {
            List<ProductViewModel> productViewModels = new List<ProductViewModel>();
            var productDBList = _IOnlineShopContext.Product.Include(s=>s.Category).ToList(); 
            productViewModels = _IMapper.Map<List<Product> , List<ProductViewModel>>(productDBList);
            return  productViewModels;
        }
    }
}
