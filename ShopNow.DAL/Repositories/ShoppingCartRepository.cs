using Microsoft.EntityFrameworkCore;
using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public ShoppingCartRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<bool> Create(ShoppingCart shoppingCart)
        {
            try
            {
                if (shoppingCart != null)
                {
                    var addProductToShoppingCart = _shopNowDbContext.Add<ShoppingCart>(shoppingCart);

                    await _shopNowDbContext.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<ShoppingCart> GetAll(Guid customerId)
        {
            try
            {
                var getShoppingCartDataByCustomerId = _shopNowDbContext.ShoppingCart.Include(a => a.Product).Where(x => x.CustomerId == customerId && x.IsDeleted == false);

                if (getShoppingCartDataByCustomerId != null)

                    return getShoppingCartDataByCustomerId;

                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteShoppingCartByCustomerId(Guid customerId)
        {
            try
            {
                var getShoppingCartDataByCustomerId = _shopNowDbContext.ShoppingCart.Where(x => x.CustomerId == customerId && x.IsDeleted == false);

                if (getShoppingCartDataByCustomerId != null)
                {
                    foreach (var shoppingcartData in getShoppingCartDataByCustomerId)
                    {
                        shoppingcartData.IsDeleted = true;
                        var obj = _shopNowDbContext.Update(shoppingcartData);
                        
                    }
                    _shopNowDbContext.SaveChangesAsync();
                    return true;
                }
     
                else return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ShoppingCart GetById(Guid shoppingCartId)
        {
            try
            {
                if (shoppingCartId != null)
                {
                    var shoppingCart = _shopNowDbContext.ShoppingCart.Where(a => a.Id == shoppingCartId).FirstOrDefault();
                    if (shoppingCart != null) return shoppingCart;
                    else return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> Update(ShoppingCart shoppingCart)
        {
            try
            {
                if (shoppingCart != null)
                {
                    var obj = _shopNowDbContext.Update(shoppingCart);
                    if (obj != null) _shopNowDbContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(ShoppingCart shoppingCart)
        {
            try
            {
                if (shoppingCart != null)
                {
                    shoppingCart.IsDeleted = true;
                    var obj = _shopNowDbContext.Update(shoppingCart);
                    _shopNowDbContext.SaveChangesAsync();
                    return true;
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
