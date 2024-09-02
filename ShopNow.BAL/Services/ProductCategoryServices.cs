using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.BAL.Services
{
    public class ProductCategoryServices
    {
        public readonly IRepository<ProductCategory> _productCategoryRepository;
        public ProductCategoryServices(IRepository<ProductCategory> productCategoryRepository)
        {
            _productCategoryRepository = productCategoryRepository;
        }

        public async Task<ProductCategory> AddProductCategory(ProductCategory productType)
        {
            try
            {
                if (productType == null)
                {
                    throw new ArgumentNullException(nameof(productType));
                }
                else
                {
                    productType.IsDeleted = false;
                    productType.CreatedOn = DateTime.Now;
                    productType.UpdatedOn = DateTime.Now;
               
                    return await _productCategoryRepository.Create(productType);
                }
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public IEnumerable<ProductCategory> GetAllProductCategories()
        {
            try
            {
                return _productCategoryRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteProductCategory(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var productCategory = _productCategoryRepository.GetById(id);
                    _productCategoryRepository.Delete(productCategory);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ProductCategory GetProductCategoryById(Guid Id)
        {
            try
            {
                return _productCategoryRepository.GetById(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateProductCategory(ProductCategory productCategory)
        {
            try
            {
                if (productCategory.Id != null || productCategory.Id != Guid.Empty)
                {
                    var obj = _productCategoryRepository.GetAll().Where(x => x.Id == productCategory.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.CategoryName = productCategory.CategoryName;
                        obj.UpdatedOn = DateTime.Now;
                        _productCategoryRepository.Update(obj);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
