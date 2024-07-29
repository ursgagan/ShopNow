using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Models
{
    public class FilterProductModel
    {
        public Guid Id { get; set; }
        public Dictionary<string, decimal?> PriceFiltervalue { get; set; }
        public Guid? ProductCategoryId { get; set; }
        public string? Color { get; set; }
        public int? Rating { get; set; }
    }
}
