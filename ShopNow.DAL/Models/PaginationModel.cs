using ShopNow.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Models
{
    public class PaginationModel
    {
        public Pager Pager { get; set; }
        public List<Product> PaginationData { get; set; } // Using List<object> for generic data

        public PaginationModel(Type entityType)
        {
            PaginationData = new List<Product>();
            // Initialize other properties as needed
        }
    }
}
