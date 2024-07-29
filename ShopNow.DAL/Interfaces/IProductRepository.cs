using ShopNow.DAL.Entities;
using ShopNow.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface IProductRepository 
    {
       public PaginationModel GetAllByPagination(int pageNumber);
       public List<RatingModel> GetRatingsByProductId(Guid Id);

       // public List<RatingModel> GetProductsByRating(int rating);
    }
}
