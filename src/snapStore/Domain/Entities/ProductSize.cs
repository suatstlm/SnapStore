using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class ProductSize: Entity<Guid>
{
    public string Size { get; set; }  
    public int StockQuantity { get; set; } 

    public virtual Product Product { get; set; }
}
