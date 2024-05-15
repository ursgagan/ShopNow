using ShopNow.DAL.Data;
using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Repositories
{
    public class RatingRepository :IRatingRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public RatingRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }
    }
}
