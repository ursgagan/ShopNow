using Microsoft.EntityFrameworkCore;
using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public ReviewRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }
        public async Task<bool> Create(ReviewModel reviewModel)
        {
            try
            {
                if (reviewModel != null)
                {
                    var order = _shopNowDbContext.ProductOrder.Include(o => o.Product).FirstOrDefault(o => o.Id == reviewModel.OrderId);

                    if (order != null)
                    {
                        Review review = new Review();
                        review.ReviewText = reviewModel.ReviewText;
                        review.OrderId = reviewModel.OrderId;
                        review.IsDeleted = false;
                        review.CreatedOn = DateTime.Now;
                        review.UpdatedOn = DateTime.Now;

                        Rating rating = new Rating();
                        rating.OrderId = reviewModel.OrderId;
                        rating.Rate = reviewModel.Rating?.Rate;
                        rating.IsDeleted = false;
                        rating.CreatedOn = DateTime.Now;
                        rating.UpdatedOn = DateTime.Now;

                        var addReview = _shopNowDbContext.Add<Review>(review);

                        var addRating = _shopNowDbContext.Add<Rating>(rating);

                        await _shopNowDbContext.SaveChangesAsync();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
