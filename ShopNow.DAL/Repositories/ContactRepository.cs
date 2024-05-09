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
    public class ContactRepository : IContactRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public ContactRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<Contact> Create(Contact contact)
        {
            try
            {
                if (contact != null)
                {
                    var addContact = _shopNowDbContext.Add<Contact>(contact);
                    await _shopNowDbContext.SaveChangesAsync();
                    return addContact.Entity;
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
