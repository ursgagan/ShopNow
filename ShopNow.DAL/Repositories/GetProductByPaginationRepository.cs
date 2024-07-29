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
    public class GetProductByPaginationRepository : IProductRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;
        public GetProductByPaginationRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public PaginationModel GetAllByPagination(int pageNumber)
        {
            try
            {
                var getProduct = _shopNowDbContext.Product.Where(x => x.IsDeleted == false).ToList();

                const int pageSize = 10;

                if (pageNumber < 1)
                {
                    pageNumber = 1;
                }

                int recordCount = getProduct.Count();

                var pager = new Pager(recordCount, pageNumber, pageSize);

                int recordSkip = (pageNumber - 1) * pageSize;

                var data = getProduct.Skip(recordSkip).Take(pager.PageSize).ToList();

                PaginationModel paginationModel = new PaginationModel(typeof(Product));

                paginationModel.PaginationData = data;
                paginationModel.Pager = pager;

                if (paginationModel != null)

                    return paginationModel;

                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<RatingModel> GetRatingsByProductId(Guid productId)
        {
            try
            {
                if (productId != Guid.Empty)
                {
                    var ratingList = (from r in _shopNowDbContext.Rating
                                      where r.IsDeleted == false
                                      join p in _shopNowDbContext.ProductOrder on r.ProductOrderId equals p.Id into p2
                                      from p in p2.DefaultIfEmpty()
                                      where p.ProductId == productId
                                      select new RatingModel
                                      {
                                          Id = r.Id,
                                          Rate = r.Rate,
                                          ProductOrderId = r.ProductOrderId,
                                          IsDeleted = r.IsDeleted,
                                          UpdatedBy = r.UpdatedBy,
                                          CreatedBy = r.CreatedBy,
                                          UpdatedOn = r.UpdatedOn,
                                          CreatedOn = r.CreatedOn,
                                      });

                    return ratingList.ToList(); 
                }
                else
                {
                    return new List<RatingModel>(); 
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }

        //public List<RatingModel> GetProductsByRating(int rating)
        //{
        //    try
        //    {
        //        //if (rating != null)
        //        //{
        //        //    var getProductByRating = 
        //        //}
                
        //        else
        //        {
        //            return new List<RatingModel>();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

    }
}
