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
    public class AdminRepository : IAdminRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public AdminRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }
        //public Admin GetUserByEmail(string email)
        //{
        //    try
        //    {
        //        if (email != null)
        //        {
        //            var admin = _shopNowDbContext.Admin.Where(x => x.EmailId == email).FirstOrDefault();
        //            if (admin != null) return admin;
        //            else return null;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}

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

                var userRoles = _shopNowDbContext.UserRoles
                                 .Where(ur => ur.CustomerId == customer.Customer.Id)
                                 .Select(ur => new UserRoles
                                 {
                                     Id = ur.Id,
                                     CustomerId = ur.CustomerId,
                                     RoleId = ur.RoleId
                                 })
                                 .FirstOrDefault();


                var customerModel = new CustomerModel
                {
                    Id = customer.Customer.Id,
                    FirstName = customer.Customer.FirstName,
                    LastName = customer.Customer.LastName,
                    EmailId = customer.Customer.EmailId,
                    Password = customer.Customer.Password,
                    IsDeleted = customer.Customer.IsDeleted,
                    AddressId = customer.Customer.AddressId,
                    UpdatedOn = customer.Customer.UpdatedOn,
                    CreatedOn = customer.Customer.CreatedOn,
                    CreatedBy = customer.Customer.CreatedBy,

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
                    },

                    UserRoles = userRoles

                };

                return customerModel; 
            }
            catch (Exception ex)
            { 
                throw ex;
            }
        }

    }
}
