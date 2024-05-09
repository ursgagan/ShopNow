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
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public ProductImageRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<ProductImages> Create(ProductImages productImage)
        {
            try
            {
                if (productImage != null)
                {
                    var addproductImage = _shopNowDbContext.Add<ProductImages>(productImage);
                    await _shopNowDbContext.SaveChangesAsync();
                    return addproductImage.Entity;
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
        public IEnumerable<ProductImages> GetAll()
        {
            try
            {
                var getProductImage = _shopNowDbContext.ProductImages.Where(x => x.IsDeleted == false).ToList();
                if (getProductImage != null) return getProductImage;
                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductImages GetById(Guid productImageId)
        {
            try
            {
                if (productImageId != null)
                {
                    var delProductImage = _shopNowDbContext.ProductImages.FirstOrDefault(x => x.Id == productImageId);
                    if (delProductImage != null) return delProductImage;
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

        public void Delete(ProductImages productImage)
        {
            try
            {
                if (productImage != null)
                {
                    productImage.IsDeleted = true;
                    var obj = _shopNowDbContext.Update(productImage);
                    _shopNowDbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void Update(ProductImages productImage)
        {
            try
            {
                if (productImage != null)
                {
                    var obj = _shopNowDbContext.Update(productImage);
                    if (obj != null) _shopNowDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductImages>> AddMultipleProductImages(List<ProductImages> productImages)
        {
            try
            {
                if (productImages != null && productImages.Any())
                {
                    await _shopNowDbContext.ProductImages.AddRangeAsync(productImages);
                    await _shopNowDbContext.SaveChangesAsync();
                    return productImages;
                }
                else
                {
                    return null;
                }
            }
            catch (DbUpdateException ex)
            {
                // Handle specific database update exceptions
                // Log or handle the exception appropriately
                throw new Exception("Error saving changes to the database.", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                // Log or handle the exception appropriately
                throw new Exception("An error occurred while adding multiple images.", ex);
            }
        }

        public IEnumerable<Product> GetAllProductWithImages()
        {
            try
            {
                var getProduct = _shopNowDbContext.Product.Include(b => b.ProductImages).ThenInclude(c => c.Image).Where(x => x.IsDeleted == false).ToList();

                if (getProduct != null)

                    return getProduct;

                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

