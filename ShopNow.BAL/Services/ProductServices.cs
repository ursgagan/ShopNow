using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Models;
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

        public readonly IProductRepository _newproductRepository;
        public ProductServices(IRepository<Product> productRepository, IProductRepository newproductRepository)
        {
            _productRepository = productRepository;
            _newproductRepository = newproductRepository;
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
                if (product.Id != null || product.Id != Guid.Empty)
                {
                    var obj = _productRepository.GetById(product.Id);
                    if (obj != null)
                    {
                        obj.Name = product.Name;
                        obj.UpdatedOn = DateTime.Now;
                        obj.ProductDescription = product.ProductDescription;
                        obj.Price = product.Price;
                        obj.Color = product.Color;
                       _productRepository.Update(obj);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public PaginationModel GetAllByPagination(int pageNumber)
        {
            try
            {
                return _newproductRepository.GetAllByPagination(pageNumber);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RatingModel> GetRatingsByProductId(Guid Id)
        {
            try
            {
                return _newproductRepository.GetRatingsByProductId(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RatingModel> GetRatingsByProductOrderId(Guid Id)
        {
            try
            {
                return _newproductRepository.GetRatingsByProductOrderId(Id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        //public List<RatingModel> GetProductsByRating(int rating)
        //{
        //    try
        //    {
        //        return _newproductRepository.GetProductsByRating(rating);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
