using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class Product : Entity<Guid>
{
    public string Name { get; set; }
    public string Code { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }

    public virtual Category Category { get; set; }
    public virtual ICollection<ProductDescription> ProductDescriptions { get; set; }
    public virtual ICollection<ProductReview>? Reviews { get; set; }
    //public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    public virtual ICollection<ProductImage> Images { get; set; }
}
