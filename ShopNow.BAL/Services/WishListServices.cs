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
    public class WishListServices
    {
        private readonly IWishListRepository _wishListRepository;
        public WishListServices(IWishListRepository wishListRepository)
        {
           _wishListRepository = wishListRepository;
        }
        public async Task<bool> AddProductToWishList(Wishlist wishlist)
        {
            try
            {
                if (wishlist == null)
                {
                    throw new ArgumentNullException(nameof(wishlist));
                }
                else
                {
                    wishlist.IsDeleted = false;
                    wishlist.CreatedOn = DateTime.Now;
                    wishlist.UpdatedOn = DateTime.Now;
                    await _wishListRepository.Create(wishlist);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Wishlist> GetWishListByCustomerId(Guid customerId)
        {
            try
            {
                return _wishListRepository.GetAll(customerId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteProductFromWishList(Guid id)
        {
            try
            {
                if (id != null || id != Guid.Empty)
                {
                    var wishListProduct = _wishListRepository.GetById(id);
                    return _wishListRepository.Delete(wishListProduct);
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
