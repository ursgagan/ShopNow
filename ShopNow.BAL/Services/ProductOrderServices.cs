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

        public IEnumerable<ProductOrder> GetMyOrdersByCustomerId(Guid customerId)
        {
            try
            {
                return _productOrderRepository.GetAll(customerId);
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
    }
}
