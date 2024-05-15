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
    public class AddressRepository : IAddressRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public AddressRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<Address> Create(Address address)
        {
            try
            {
                if (address != null)
                {
                    var addAddress = _shopNowDbContext.Add<Address>(address);
                    await _shopNowDbContext.SaveChangesAsync();
                    return addAddress.Entity;
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

    }
}
