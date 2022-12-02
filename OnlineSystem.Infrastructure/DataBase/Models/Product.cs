using System;
using System.Collections.Generic;

namespace OnlineSystem.Infrastructure.DataBase.Models
{
    public partial class Product
    {
        public int ID { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public bool? HasAvailableStock { get; set; }
        public int? CategoryID { get; set; }

        public virtual Category? Category { get; set; }
    }
}
