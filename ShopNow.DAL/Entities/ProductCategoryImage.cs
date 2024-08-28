using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopNow.DAL.Entities
{
    public class ProductCategoryImage
    {
        public Guid Id { get; set; }
        public Guid? ProductCategoryId { get; set; }
        public Guid? ImageId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public virtual CImage CImage { get; set; }
    }
}
