using ShopNow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Models
{
    public class RatingModel
    {
        public Guid Id { get; set; }
        public Guid? ProductOrderId { get; set; }
        public int? Rate { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public virtual List<ProductOrder> ProductOrders { get; set; }
        public virtual List<Rating> Rating{ get; set; }

    }
}
