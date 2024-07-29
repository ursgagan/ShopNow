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
    public class ProductOrderServices
    {
        private readonly IProductOrderRepository _productOrderRepository;
        public ProductOrderServices(IProductOrderRepository productOrderRepository)
        {
            _productOrderRepository = productOrderRepository;
        }

        public async Task<bool> AddProductOrder(List<ProductOrder> productOrderList)
        {
            try
            {
                if (productOrderList == null)
                {
                    throw new ArgumentNullException(nameof(productOrderList));
                }
                else
                {
                    await _productOrderRepository.Create(productOrderList);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ProductOrderModel> GetMyOrdersByCustomerId(Guid customerId)
        {
            try
            {
                return (IEnumerable<ProductOrderModel>)_productOrderRepository.GetMyOrdersByCustomerId(customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductOrder GetProductOrderByProductId(Guid productId)
        {
            try
            {
                return _productOrderRepository.GetProductOrderByProductId(productId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ProductOrder> GetAllMyOrders()
        {
            try
            {
                return _productOrderRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductOrder GetProductOrderById(Guid orderId)
        {
            try
            {
                return _productOrderRepository.GetById(orderId);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool updateProductOrderStatus(string orderId, string orderStatus)
        {
            try
            {
                if (orderId != null)
                {
                    var productOrderData = _productOrderRepository.GetById(new Guid(orderId));
                    if (productOrderData != null)
                    {
                        productOrderData.Status = orderStatus;
                        
                        _productOrderRepository.UpdateProductOrderStatus(productOrderData);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;

        }
        //public void UpdateOrderStatus()
        //{
        //    try
        //    {
        //        if (product.Id != null || product.Id != Guid.Empty)
        //        {
        //            var obj = _productRepository.GetById(product.Id);
        //            if (obj != null)
        //            {
        //                obj.Name = product.Name;
        //                obj.UpdatedOn = DateTime.Now;
        //                obj.ProductDescription = product.ProductDescription;
        //                obj.Price = product.Price;
        //                obj.Color = product.Color;
        //                _productRepository.Update(obj);
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
