using ShopNow.DAL.Entities;
using ShopNow.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface IAdminRepository
    {
        public CustomerModel GetUserByEmail(string email);
    }
}
