using Microsoft.EntityFrameworkCore;
using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Repositories
{
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ShopNowDbContext _shopNowDbContext;

        public ComplaintRepository(ShopNowDbContext shopNowDbContext)
        {
            _shopNowDbContext = shopNowDbContext;
        }

        public async Task<Complaint> AddComplaint(Complaint productComplaint)
        {
            try
            {
                if (productComplaint != null)
                {
                    var addComplaint = _shopNowDbContext.Add<Complaint>(productComplaint);
                    await _shopNowDbContext.SaveChangesAsync();
                    return addComplaint.Entity;
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

        public IEnumerable<Complaint> GetAll()
        {
            try
            {
                var getProductComplaints = _shopNowDbContext.Complaint.Include(a => a.ProductOrder)
                .ThenInclude(b => b.Product).Where(x => x.IsDeleted == false).ToList();

                if (getProductComplaints != null)

                    return getProductComplaints;

                else return null;
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
