using Microsoft.AspNetCore.Http;
using OnlineSystem.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OnlineSystem.Service.Common;

namespace OnlineSystem.Service.Interfaces
{
    public interface IProductService
    {
        public List<ProductViewModel> GetProductListMoc();
        public List<ProductViewModel> GetProductList(DataTableParameters dataTableAjaxPostModel, out int TotalRecordsCount, out int FilteredRecordCount);


        public List<CategoryViewModel>   GetCategoryList(bool IsParentCategory = false);
        public  ResultViewModel SaveProduct(ProductCreateViewModel productCreateViewModel);


        public List<ProductStatus> GetProductStatusList();


    }
}
