using ShopNow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface IProductCategoryImageRepository
    {
        public Task<ProductCategoryImage> Create(ProductCategoryImage productCategoryImage);
        public IEnumerable<ProductCategoryImage> GetAll();
        public ProductCategoryImage GetById(Guid Id);
        public void Update(ProductCategoryImage productCategoryImage);    
        public void Delete(ProductCategoryImage productCategoryImage);
    }
}
