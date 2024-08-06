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
    public class AdminRepository : IAdminRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public AdminRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }
        public Admin GetUserByEmail(string email)
        {
            try
            {
                if (email != null)
                {
                    var admin = _shopNowDbContext.Admin.Where(x => x.EmailId == email).FirstOrDefault();
                    if (admin != null) return admin;
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
    }
}
