using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Models;
using ShopNow.DAL.Repositories;
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

        public async Task<bool> AddReview(ReviewModel reviewModel)
        {
            try
            {
                if (reviewModel == null)
                {
                    throw new ArgumentNullException(nameof(reviewModel));
                }
                else
                {
                    await _reviewRepository.Create(reviewModel);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
