using Microsoft.EntityFrameworkCore;
using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Models;
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

        public CustomerModel GetUserByEmail(string email)
        {
            try
            {
                if (email == null)
                {
                    return null;
                }

                var customer = (from r in _shopNowDbContext.Customer
                                where r.IsDeleted == false && r.EmailId == email
                                join p in _shopNowDbContext.Address on r.AddressId equals p.Id
                                select new
                                {
                                    Customer = r,
                                    Address = p
                                })
                                .FirstOrDefault();

                if (customer == null)
                {
                    return null;
                }

                // Map the results to CustomerModel
                var customerModel = new CustomerModel
                {
                    Id = customer.Customer.Id,
                    FirstName = customer.Customer.FirstName,
                    LastName = customer.Customer.LastName,
                    EmailId = customer.Customer.EmailId,
                    IsDeleted = customer.Customer.IsDeleted,
                    AddressId = customer.Customer.AddressId,
                    UpdatedOn = customer.Customer.UpdatedOn,
                    CreatedOn = customer.Customer.CreatedOn,
                    CreatedBy = customer.Customer.CreatedBy,
                    // Exclude UpdatedBy if it's not present in the database

                    Address = new Address
                    {
                        Id = customer.Address.Id,
                        PhoneNumber = customer.Address.PhoneNumber,
                        Address1 = customer.Address.Address1,
                        Address2 = customer.Address.Address2,
                        Country = customer.Address.Country,
                        City = customer.Address.City,
                        State = customer.Address.State,
                        ZipCode = customer.Address.ZipCode,
                        IsDeleted = customer.Address.IsDeleted,

                        UpdatedOn = customer.Address.UpdatedOn,
                        CreatedOn = customer.Address.CreatedOn,
                    }


                };


                return customerModel;
            }
            catch (Exception ex)
            {
                // Log the exception (ex) if needed
                throw;
            }
        }

        //public Customer GetUserByEmail(string email)
        //{
        //    try
        //    {
        //        if (email != null)
        //        {
        //            var customer = _shopNowDbContext.Customer.Include(c => c.Address)       
        //                           .Include(c => c.UserRoles)    
        //                           .Where(c => c.EmailId == email)
        //                           .FirstOrDefault(); if (customer != null) return customer;
        //            else return null;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public Customer GetById(Guid customerId)
        {
            try
            {
                if (customerId != null)
                {
                    var customer = _shopNowDbContext.Customer.Include(a => a.Address).Where(a => a.Id == customerId).FirstOrDefault();
                    if (customer != null) return customer;
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

        public void Update(Customer customer)
        {
            try
            {
                if (customer != null)
                {
                    var obj = _shopNowDbContext.Update(customer);
                    if (obj != null) _shopNowDbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Customer GetCustomerByResetCode(string resetCode)
        {
            try
            {
                if (resetCode != null)
                {
                    var user = _shopNowDbContext.Customer.FirstOrDefault(x => x.ResetCode == resetCode);
                    if (user != null) return user;
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

        public IEnumerable<Customer>GetAll()
        {
            try
            {
                var getAllCustomers = _shopNowDbContext.Customer.Where(x => x.IsDeleted == false).ToList();

                if (getAllCustomers != null)

                    return getAllCustomers;

                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }
        //public Customer GetCustomerDataByProductComplaint(Guid complaintId)
        //{
        //    try
        //    {
        //        var getCustomerDataByComplaintId = _shopNowDbContext.Complaint.Include(a => a.ProductOrder).ThenInclude(b => b.Customer).Where(c => c.IsDeleted == false).ToList();

        //        if (getCustomerDataByComplaintId != null)

        //            return getCustomerDataByComplaintId;

        //        else return null;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

    }
}
