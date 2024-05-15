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
    public class CustomerServices
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerServices(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<Customer>AddCustomer(Customer customer)
        {
            try
            {
                if (customer == null)
                {
                    throw new ArgumentNullException(nameof(customer));
                }
                else
                {
                    customer.IsDeleted = false;
                    customer.CreatedOn = DateTime.Now;
                    customer.UpdatedOn = DateTime.Now;
                    return await _customerRepository.Create(customer);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
