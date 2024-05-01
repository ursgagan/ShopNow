using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Repositories
{
    public class ProductCategoryRepository : IRepository<ProductCategory>
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public ProductCategoryRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<ProductCategory> Create(ProductCategory productType)
        {
            try
            {
                if (productType != null)
                {
                    var addProductType = _shopNowDbContext.Add<ProductCategory>(productType);
                    await _shopNowDbContext.SaveChangesAsync();
                    return addProductType.Entity;
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

        public IEnumerable<ProductCategory> GetAll()
        {
            try
            {
                var getProductCategories= _shopNowDbContext.ProductCategory.Where(x => x.IsDeleted == false).ToList();
                if (getProductCategories != null) return getProductCategories;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductCategory GetById(Guid productTypeId)
        {
            try
            {
                if (productTypeId != null)
                {
                    var delProductCategory = _shopNowDbContext.ProductCategory.FirstOrDefault(x => x.Id == productTypeId);
                    if (delProductCategory != null) return delProductCategory;
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
        public void Delete(ProductCategory productCategory)
        {
            try
            {
                if (productCategory != null)
                {
                    productCategory.IsDeleted = true;
                    var obj = _shopNowDbContext.Update(productCategory);
                    _shopNowDbContext.SaveChangesAsync();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(ProductCategory productCategory)
        {
            try
            {
                if (productCategory != null)
                {
                    var obj = _shopNowDbContext.Update(productCategory);
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
