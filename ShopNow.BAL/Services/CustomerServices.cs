using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Models;
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
        public Customer isUserExist(string email)
        {
            return _customerRepository.GetUserByEmail(email);
        }
        public Customer GetCustomerById(Guid user)
        {
            try
            {
                return _customerRepository.GetById(user);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void UpdateCustomer(Customer customer)
        {
            try
            {
                if (customer.Id != null || customer.Id != Guid.Empty)
                {
                    var obj = _customerRepository.GetById(customer.Id);
                    if (obj != null)
                    {
                        obj.FirstName = customer.FirstName;
                        obj.LastName = customer.LastName;           
                        obj.UpdatedOn = DateTime.Now;
                        obj.ResetCode = customer.ResetCode;
                        obj.Password = customer.Password;
                        if(customer.Address != null)
                        {

                            obj.Address.Address1 = customer.Address.Address1;
                        obj.Address.Address2 = customer.Address.Address2;
                        obj.Address.PhoneNumber = customer.Address.PhoneNumber;
                        obj.Address.ZipCode = customer.Address.ZipCode;
                        obj.Address.UpdatedOn = customer.Address.UpdatedOn;
                                 
                        }
                        _customerRepository.Update(obj);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Customer GetUserByResetCode(string resetCode)
        {
            try
            {
                return _customerRepository.GetCustomerByResetCode(resetCode);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            try
            {
                return _customerRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }


        //public Customer GetCustomerDataByProductComplaint(Guid Id)
        //{
        //    try
        //    {
        //        return _customerRepository.GetCustomerDataByProductComplaint(Id);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}
