using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.BAL.Services
{
    public class ContactServices
    {
        private readonly IContactRepository _contactRepository;
        public ContactServices(IContactRepository contactrepository)
        {
            _contactRepository = contactrepository;
        }
        public async Task<Contact> AddContact(Contact contact)
        {
            try
            {
                if (contact == null)
                {
                    throw new ArgumentNullException(nameof(contact));
                }
                else
                {
                    return await _contactRepository.Create(contact);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
