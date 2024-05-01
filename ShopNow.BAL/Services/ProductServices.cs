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
    public class ProductServices
    {
        public readonly IRepository<Product> _productRepository;
        public ProductServices(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddProduct(Product product)
        {
            try
            {
                if (product == null)
                {
                    throw new ArgumentNullException(nameof(product));
                }
                else
                {
                    product.IsDeleted = false;
                    product.CreatedOn = DateTime.Now;
                    product.UpdatedOn = DateTime.Now;

                    return await _productRepository.Create(product);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<Product> GetAllProduct()
        {
            try
            {
                return _productRepository.GetAll().ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void DeleteProduct(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var product = _productRepository.GetById(id);
                    _productRepository.Delete(product);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Product GetProductById(Guid Id)
        {
            try
            {
                return _productRepository.GetById(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateProduct(Product product)
        {
            try
            {
                if (product.ProductId != null || product.ProductId != Guid.Empty)
                {
                    var obj = _productRepository.GetAll().Where(x => x.ProductId == product.ProductId).FirstOrDefault();
                    if (obj != null)
                    {
                        obj.Name = product.Name;
                        obj.UpdatedOn = DateTime.Now;
                        _productRepository.Update(obj);
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
