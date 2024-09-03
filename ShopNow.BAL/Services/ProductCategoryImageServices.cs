using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.BAL.Services
{
    public class ProductCategoryImageServices
    {
        private readonly IProductCategoryImageRepository _productCategoryImageRepository;
        public ProductCategoryImageServices(IProductCategoryImageRepository productCategoryImageRepository)
        {
            _productCategoryImageRepository = productCategoryImageRepository;
        }

        public async Task<ProductCategoryImage> AddProductCategoryImage(ProductCategoryImage productCategoryImage)
        {
            try
            {
                if (productCategoryImage == null)
                {
                    throw new ArgumentNullException(nameof(productCategoryImage));
                }
                else
                {
                    productCategoryImage.IsDeleted = false;
                    productCategoryImage.CreatedOn = DateTime.Now;
                    productCategoryImage.UpdatedOn = DateTime.Now;
                    return await _productCategoryImageRepository.Create(productCategoryImage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductCategoryImage> GetAllProductCategoryImage()
        {
            try
            {
                return _productCategoryImageRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteProductCategoryImage(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var productCategoryImage = _productCategoryImageRepository.GetById(id);
                    _productCategoryImageRepository.Delete(productCategoryImage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ProductCategoryImage GetProductCategoryImageById(Guid Id)
        {
            try
            {
                return _productCategoryImageRepository.GetById(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductCategoryImage UpdateProductCategoryImage(ProductCategoryImage productCategoryImage)
        {
            try
            {
                if (productCategoryImage.Id != null || productCategoryImage.Id != Guid.Empty)
                {
                    var obj = _productCategoryImageRepository.GetAll().Where(x => x.Id == productCategoryImage.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.UpdatedOn = DateTime.Now;
                        _productCategoryImageRepository.Update(obj);
                    }
                }
                return productCategoryImage;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ProductCategoryImage>> AddMultipleProductCategoryImages(List<ProductCategoryImage> productCategoryImage)
        {
            try
            {
                if (productCategoryImage == null)
                {
                    throw new ArgumentNullException(nameof(productCategoryImage));
                }
                else
                {
                    foreach (var imageItem in productCategoryImage)
                    {

                        imageItem.IsDeleted = false;
                        imageItem.CreatedOn = DateTime.Now;
                        imageItem.UpdatedOn = DateTime.Now;
                    }
                    return await _productCategoryImageRepository.AddMultipleProductCategoryImages(productCategoryImage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
