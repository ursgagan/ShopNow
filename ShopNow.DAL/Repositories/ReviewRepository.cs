using ShopNow.DAL.Data;
using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Repositories
{
    public class ReviewRepository: IReviewRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public ReviewRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }
    }
}
