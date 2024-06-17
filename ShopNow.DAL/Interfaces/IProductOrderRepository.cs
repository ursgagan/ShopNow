using ShopNow.DAL.Entities;
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
        public IEnumerable<ProductOrder> GetAll(Guid customerId);
        public ProductOrder GetProductOrderByProductId(Guid productId);
        public IEnumerable<ProductOrder> GetAll();
    }
}
