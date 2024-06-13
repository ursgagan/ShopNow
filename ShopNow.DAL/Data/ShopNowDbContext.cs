using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShopNow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Data
{
    public class ShopNowDbContext : IdentityDbContext
    {
        public ShopNowDbContext(DbContextOptions<ShopNowDbContext> options)
        : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Contact> Contact { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<Wishlist> WishList { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }
    }
}
