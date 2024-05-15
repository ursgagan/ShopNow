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
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public CustomerRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<Customer> Create(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    var addcustomer = _shopNowDbContext.Add<Customer>(customer);
                    await _shopNowDbContext.SaveChangesAsync();
                    return addcustomer.Entity;
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
