using ShopNow.DAL.Entities;
using ShopNow.DAL.Interfaces;
using ShopNow.DAL.Models;
using ShopNow.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.BAL.Services
{
    public class ComplaintServices
    {
        private readonly IComplaintRepository _complaintRepository;
        public ComplaintServices(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }

        public async Task<Complaint> AddComplaint(Complaint productComplaint)
        {
            try
            {
                if (productComplaint == null)
                {
                    throw new ArgumentNullException(nameof(productComplaint));
                }
                else
                {
                    productComplaint.IsDeleted = false;
                    productComplaint.CreatedOn = DateTime.Now;
                    productComplaint.UpdatedOn = DateTime.Now;
                    await _complaintRepository.AddComplaint(productComplaint);                
                }
            }
            catch (Exception)
            {
               throw;
            }
            return productComplaint;
        }

        public IEnumerable<Complaint> GetAllProductComplaints()
        {
            try
            {
                return _complaintRepository.GetAll();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateComplaintStatus(string complaintId, string complaintStatus)
        {
            try
            {
                if (complaintId != null)
                {
                    var complaintData = _complaintRepository.GetById(new Guid(complaintId));
                    if (complaintData != null)
                    {
                        complaintData.Status = complaintStatus;

                        _complaintRepository.UpdateComplaintStatus(complaintData);
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

    }
}
