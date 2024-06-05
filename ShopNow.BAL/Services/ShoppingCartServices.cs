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
    public class ShoppingCartServices
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        public ShoppingCartServices(IShoppingCartRepository shoppingCartRepository)
        {
            _shoppingCartRepository = shoppingCartRepository;
        }

        public async Task<bool> AddProductToShoppingCart(ShoppingCart shoppingCart)
        {
            try
            {
                if (shoppingCart == null)
                {
                    throw new ArgumentNullException(nameof(shoppingCart));
                }
                else
                {
                    shoppingCart.IsDeleted = false;
                    shoppingCart.CreatedOn = DateTime.Now;
                    shoppingCart.UpdatedOn = DateTime.Now;
                    await _shoppingCartRepository.Create(shoppingCart);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ShoppingCart> GetAllProductByCustomerId(Guid customerId)
        {
            try
            {
                return _shoppingCartRepository.GetAll(customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateShoppingCart(ShoppingCart shoppingCart)
        {
            try
            {
                if (shoppingCart.Id != null || shoppingCart.Id != Guid.Empty)
                {
                    var obj = _shoppingCartRepository.GetById(shoppingCart.Id);
                    if (obj != null)
                    {
                        obj.Quantity = shoppingCart.Quantity;
                        obj.UpdatedOn = DateTime.Now;
                        obj.TotalPrice = shoppingCart.TotalPrice;
                        
                        _shoppingCartRepository.Update(obj);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeleteProductFromShoppingCart(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var ShoppingCartProduct = _shoppingCartRepository.GetById(id);
                    return _shoppingCartRepository.Delete(ShoppingCartProduct);  
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
