using ShopNow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Interfaces
{
    public interface IComplaintRepository
    {
        public Task<Complaint> AddComplaint(Complaint productComplaint);
        public IEnumerable<Complaint> GetAll();
        public Complaint GetById(Guid complaintId);
        public bool UpdateComplaintStatus(Complaint complaint);
       
    }
}
