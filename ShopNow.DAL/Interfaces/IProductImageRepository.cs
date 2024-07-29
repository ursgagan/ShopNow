using ShopNow.DAL.Entities;
using ShopNow.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface IProductImageRepository : IRepository<ProductImages>
    {      
        public Task<List<ProductImages>> AddMultipleProductImages(List<ProductImages> productImage);

        public IEnumerable<Product> GetAllProductWithImages();

        public IEnumerable<Product> GetProductByFilters(FilterProductModel filterProductModel);
    }
}
