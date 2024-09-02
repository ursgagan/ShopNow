using Microsoft.EntityFrameworkCore;
using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Repositories
{
    public class ProductCategoryImageRepository : IProductCategoryImageRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public ProductCategoryImageRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }
        public async Task<ProductCategoryImage> Create(ProductCategoryImage productCategoryImage)
        {
            try
            {
                if (productCategoryImage != null)
                {
                    var addproductCategoryImage = _shopNowDbContext.Add<ProductCategoryImage>(productCategoryImage);
                    await _shopNowDbContext.SaveChangesAsync();
                    return addproductCategoryImage.Entity;
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
        public IEnumerable<ProductCategoryImage> GetAll()
        {
            try
            {
                var getProductCategoryImage = _shopNowDbContext.ProductCategoryImage.Where(x => x.IsDeleted == false).ToList();
                if (getProductCategoryImage != null) return getProductCategoryImage;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductCategoryImage GetById(Guid productImageId)
        {
            try
            {
                if (productImageId != null)
                {
                    var delProductCategoryImage = _shopNowDbContext.ProductCategoryImage.FirstOrDefault(x => x.Id == productImageId);
                    if (delProductCategoryImage != null) return delProductCategoryImage;
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

        public void Delete(ProductCategoryImage productCategoryImage)
        {
            try
            {
                if (productCategoryImage != null)
                {
                    productCategoryImage.IsDeleted = true;
                    var obj = _shopNowDbContext.Update(productCategoryImage);
                    _shopNowDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(ProductCategoryImage productCategoryImage)
        {
            try
            {
                if (productCategoryImage != null)
                {
                    var obj = _shopNowDbContext.Update(productCategoryImage);
                    if (obj != null) _shopNowDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductCategoryImage>> AddMultipleProductImages(List<ProductCategoryImage> productCategoryImages)
        {
            try
            {
                if (productCategoryImages != null && productCategoryImages.Any())
                {
                    await _shopNowDbContext.ProductCategoryImage.AddRangeAsync(productCategoryImages);
                    await _shopNowDbContext.SaveChangesAsync();
                    return productCategoryImages;
                }
                else
                {
                    return null;
                }
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Error saving changes to the database.", ex);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding multiple images.", ex);
            }
        }

    }
}
