using Microsoft.EntityFrameworkCore;
using ShopNow.DAL.Data;
using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Models;
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
                if (productComplaint.ComplaintHeadLine != null && productComplaint.ComplaintDescription != null)
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

        public Complaint GetById(Guid complaintId)
        {
            try
            {
                if (complaintId != null)
                {
                    var getComplaintById = _shopNowDbContext.Complaint.Where(a => a.Id == complaintId).FirstOrDefault();
                    if (getComplaintById != null) return getComplaintById;
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
        public bool UpdateComplaintStatus(Complaint complaint)
        {
            try
            {
                if (complaint.Id != null)
                {
                    var complaintData = _shopNowDbContext.Update(complaint);
                    if (complaintData != null) _shopNowDbContext.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    
    }
}
