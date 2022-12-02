using System;
using System.Collections.Generic;

namespace OnlineSystem.Infrastructure.DataBase.Models
{
    public partial class Category
    {
        public Category()
        {
            InverseParentCategory = new HashSet<Category>();
            Product = new HashSet<Product>();
        }

        public int CategoryID { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ParentCategoryID { get; set; }

        public virtual Category? ParentCategory { get; set; }
        public virtual ICollection<Category> InverseParentCategory { get; set; }
        public virtual ICollection<Product> Product { get; set; }
    }
}
