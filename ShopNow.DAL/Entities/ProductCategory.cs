namespace ShopNow.DAL.Entities
{
    public class ProductCategory
    {
        public Guid Id { get; set; }
        public string? CategoryName { get; set; } = string.Empty;
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }

        public virtual ProductCategoryImage ProductCategoryImage { get; set; }
    }
}
