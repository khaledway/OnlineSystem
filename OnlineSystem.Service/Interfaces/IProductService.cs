using OnlineSystem.Service.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSystem.Service.Interfaces
{
    public interface IProductService
    {
        public List<ProductViewModel> GetProductListMoc();
        public List<ProductViewModel> GetProductList();


    }
}
