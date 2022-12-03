using Microsoft.EntityFrameworkCore;
using OnlineSystem.Infrastructure.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineSystem.Infrastructure
{
    public interface IOnlineShopContext
    {

          DbSet<Category> Category { get; set; }
          DbSet<Product> Product { get; set; }

    }
}
