using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using sInvent.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sInvent.Database
{
   public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        { }

        public DbSet<Order> Orders{ get; set; }
        public DbSet<OrderStock> OrderStocks{ get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Stock> Stock{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<OrderStock>().HasKey(os => new { os.OrderId,os.StockId});
        }

    }
}
