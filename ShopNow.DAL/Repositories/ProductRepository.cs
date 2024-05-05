using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public ProductRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<Product> Create(Product product)
        {
            try
            {
                if (product != null)
                {
                    var addProduct = _shopNowDbContext.Add<Product>(product);
                    await _shopNowDbContext.SaveChangesAsync();
                    return addProduct.Entity;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Product> GetAll()
        {
            try
            {
                var getProduct = _shopNowDbContext.Product.Where(x => x.IsDeleted == false).ToList();

                if (getProduct != null)

                return getProduct;

                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Product GetById(Guid productId)
        {
            try
            {
                if (productId != null)
                {
                    var delProduct = _shopNowDbContext.Product.FirstOrDefault(x => x.Id == productId);
                    if (delProduct != null) return delProduct;
                    else return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Delete(Product product)
        {
            try
            {
                if (product != null)
                {
                    product.IsDeleted = true;
                    var obj = _shopNowDbContext.Update(product);
                    _shopNowDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(Product product)
        {
            try
            {
                if (product != null)
                {
                    var obj = _shopNowDbContext.Update(product);
                    if (obj != null) _shopNowDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

       

    }
}