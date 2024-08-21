using ShopNow.DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Entities
{
    public class Customer
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; } 
        public string? EmailId { get; set; }
        public Guid? AddressId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string? Password { get; set; }
        public string? ResetCode { get; set; }
        public virtual Address Address { get; set; }

        [NotMapped]
        public virtual UserRoles UserRoles { get; set; }

    }
}
