using ShopNow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface IWishListRepository
    {
        public Task<bool> Create(Wishlist wishlist);
        public IEnumerable<Wishlist> GetAll(Guid customerId);
        public Wishlist GetById(Guid Id);
        public bool Delete(Wishlist wishlist);
    }
}
