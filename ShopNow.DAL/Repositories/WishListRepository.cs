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
    public class WishListRepository : IWishListRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public WishListRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<bool> Create(Wishlist wishlist)
        {
            try
            {
                if (wishlist != null)
                {
                    var addProductToWishList = _shopNowDbContext.Add<Wishlist>(wishlist);

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


        public IEnumerable<Wishlist> GetAll(Guid customerId)
        {
            try
            {
                var getWishListDataByCustomerId = _shopNowDbContext.WishList.Include(a => a.Product).Where(x => x.CustomerId == customerId && x.IsDeleted == false);

                if (getWishListDataByCustomerId != null)

                    return getWishListDataByCustomerId;

                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Wishlist GetById(Guid wishListId)
        {
            try
            {
                if (wishListId != null)
                {
                    var wishlist = _shopNowDbContext.WishList.Where(a => a.Id == wishListId).FirstOrDefault();
                    if (wishlist != null) return wishlist;
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

        public bool Delete(Wishlist wishlist)
        {
            try
            {
                if (wishlist != null)
                {
                    wishlist.IsDeleted = true;
                    var obj = _shopNowDbContext.Update(wishlist);
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
