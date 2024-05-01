using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Entities
{
    public class ProductImages
    {
        public Guid ProductImageId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ImageId { get; set; }
    }
}
