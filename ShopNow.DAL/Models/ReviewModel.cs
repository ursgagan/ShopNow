using ShopNow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Models
{
    public class ReviewModel
    {
        public Guid Id { get; set; }      
        public Guid? ProductOrderId { get; set; }
        public string? ReviewText { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public virtual Rating? Rating { get; set; }
    }
}
