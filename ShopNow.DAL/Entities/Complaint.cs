using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Entities
{
    public class Complaint
    {
        public Guid Id { get; set; }
        public string ComplaintHeadLine { get; set; }
        public string ComplaintDescription { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; } 
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? ProductOrderId { get; set; }
        public virtual ProductOrder? ProductOrder { get; set; }  
        public string? Status { get; set; } 
    }
}
