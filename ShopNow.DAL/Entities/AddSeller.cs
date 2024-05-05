using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Entities
{
    public class AddSeller
    {
        public Guid? SellerId { get; set; } 
        public string? SellerFirstName { get; set;}
        public string SellerLastName { get; set; }
        public int PhoneNumber { get; set;}
        public string EmailId { get; set;}  
        public string SellerGSTIn { get; set;}
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public DateTime? CreatedBy { get; set; }
        public DateTime? UpdatedBy { get; set; }

    }
}
