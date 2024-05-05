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
    public class ProductImageServices
    {

        private readonly IProductImageRepository _productimageRepository;
        public ProductImageServices(IProductImageRepository productimageRepository)
        {
            _productimageRepository = productimageRepository;
        }

        public async Task<ProductImages> AddProductImage(ProductImages productImage)
        {
            try
            {
                if (productImage == null)
                {
                    throw new ArgumentNullException(nameof(productImage));
                }
                else
                {
                    productImage.IsDeleted = false;
                    productImage.CreatedOn = DateTime.Now;
                    productImage.UpdatedOn = DateTime.Now;
                    return await _productimageRepository.Create(productImage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductImages> GetAllProductImage()
        {
            try
            {
                return _productimageRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteProductImage(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var productImage = _productimageRepository.GetById(id);
                    _productimageRepository.Delete(productImage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public ProductImages GetProductImageById(Guid Id)
        {
            try
            {
                return _productimageRepository.GetById(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductImages UpdateProductImage(ProductImages productImage)
        {
            try
            {
                if (productImage.Id != null || productImage.Id != Guid.Empty)
                {
                    var obj = _productimageRepository.GetAll().Where(x => x.Id == productImage.Id).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.UpdatedOn = DateTime.Now;
                        _productimageRepository.Update(obj);
                    }
                }
                return productImage;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<ProductImages>> AddMultipleProductImages(List<ProductImages> productImage)
        {
            try
            {
                if (productImage == null)
                {
                    throw new ArgumentNullException(nameof(productImage));
                }
                else
                {
                    foreach (var imageItem in productImage)
                    {

                        imageItem.IsDeleted = false;
                        imageItem.CreatedOn = DateTime.Now;
                        imageItem.UpdatedOn = DateTime.Now;
                    }
                    return await _productimageRepository.AddMultipleProductImages(productImage);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}

