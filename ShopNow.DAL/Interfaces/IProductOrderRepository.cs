using ShopNow.DAL.Entities;
using ShopNow.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface IProductOrderRepository
    {
        public Task<bool> Create(List<ProductOrder> productOrderList);
        public IEnumerable<ProductOrderModel> GetMyOrdersByCustomerId(Guid customerId);
        public ProductOrder GetProductOrderByProductId(Guid productId);
        public IEnumerable<ProductOrder> GetAll();
        public ProductOrder GetById(Guid orderId);
        public bool UpdateProductOrderStatus(ProductOrder productOrder);

    }
}
