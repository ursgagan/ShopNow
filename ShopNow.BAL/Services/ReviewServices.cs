using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.BAL.Services
{
    public class ReviewServices
    {
        private readonly IReviewRepository _reviewRepository;
        public ReviewServices(IReviewRepository reviewrepository)
        {
            _reviewRepository = reviewrepository;
        }
    }
}
