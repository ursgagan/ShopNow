using ShopNow.DAL.Entities;
using ShopNow.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface ICustomerRepository
    {
        public Task<Customer> Create(Customer customer);
        public Customer GetUserByEmail(string email);
        public Customer GetById(Guid Id);
        public void Update(Customer customer);
        public Customer GetCustomerByResetCode(string resetCode);
        public IEnumerable<Customer> GetAll();
        //public Customer GetCustomerDataByProductComplaint(Guid Id);
    }
}
