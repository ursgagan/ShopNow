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
    public class AddressServices
    {
        private readonly IAddressRepository _addressRepository;
        public AddressServices(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }

        public async Task<Address> AddAddress(Address address)
        {
            try
            {
                if (address == null)
                {
                    throw new ArgumentNullException(nameof(address));
                }
                else
                {
                    address.IsDeleted = false;
                    address.CreatedOn = DateTime.Now;
                    address.UpdatedOn = DateTime.Now;
                    address.Country = "India";
                    address.State = "Panjab";
                    address.City = "Fatehgarh Sahib";
                    return await _addressRepository.Create(address);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
