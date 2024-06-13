using ShopNow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface IShoppingCartRepository
    {
        public Task<bool> Create(ShoppingCart shoppingCart);
        public IEnumerable<ShoppingCart> GetAll(Guid customerId);
        public ShoppingCart GetById(Guid Id);
        public Task<bool> Update(ShoppingCart shoppingCart);
        public bool Delete(ShoppingCart shoppingCart);
        public bool DeleteShoppingCartByCustomerId(Guid customerId);
    }
}
