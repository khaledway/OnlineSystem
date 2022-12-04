using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSystem.Service.ViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public string? Name_ar { get; set; }
        public double? Price { get; set; }
        public bool? HasAvailableStock { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ParentCategoryName { get; set; }

    }
    public class ProductCreateViewModel
    {
        

        [Required]
        public string? Description { get; set; }
       
        [Required]
        public string? Name { get; set; }


        [Required]
        public string? Name_ar { get; set; }


        public double? Price { get; set; }
        public bool? HasAvailableStock { get; set; }
        public int? CategoryID { get; set; }


        public IFormFile? ImageFile { get; set; }

        public string? ProductImageName { get; set; }

        public string? ProductImagePath { get; set; }
    }



    public class ProductStatus
    {

        public int StatusID { get; set; }
        public  string StatusName { get; set; }
    }



    }
