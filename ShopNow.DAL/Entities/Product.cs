using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Entities
{
    public class Product
    {
        public Guid Id { get; set; }        
        public string? Name { get; set; }
        public Guid? ProductCategoryId { get; set; }
        public decimal? Price { get; set; }
        public Guid? SupplierId { get; set; }
        //public string? Gender { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public Guid? ImageId { get; set; }
        public string? ProductDescription { get; set; } 
    }
}
