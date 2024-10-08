﻿using Microsoft.EntityFrameworkCore;
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

        public Customer GetUserByEmail(string email)
        {
            try
            {
                if (email != null)
                {
                    var customer = _shopNowDbContext.Customer.Include(a => a.Address).Where(x => x.EmailId == email).FirstOrDefault();
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

        public IEnumerable<Customer> GetAll()
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
