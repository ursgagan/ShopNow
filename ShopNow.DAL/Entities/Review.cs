using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid? CustomerId { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public int? ReviewText { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public virtual Rating? Rating { get; set; }
    }
}
