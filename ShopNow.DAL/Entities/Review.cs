using ShopNow.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Entities
{
    public class Review
    {
        public Guid Id { get; set; }
        public Guid ProductOrderId { get; set; }     
        public string? ReviewText { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        [NotMapped]
        public virtual List<RatingModel> Ratings { get; set; }  
        public virtual ProductOrder ProductOrder { get; set; }


    }
}
