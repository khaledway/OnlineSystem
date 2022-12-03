using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSystem.Service.ViewModels
{
    public class ProductViewModel
    {
        public int ID { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public bool? HasAvailableStock { get; set; }
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }

    }
}
