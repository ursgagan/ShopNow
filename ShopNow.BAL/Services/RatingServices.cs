using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.BAL.Services
{
    public class RatingServices
    {
        private readonly IRatingRepository _ratingRepository;
        public RatingServices(IRatingRepository ratingRepository)
        {
            _ratingRepository = ratingRepository;
        }
    }
}
