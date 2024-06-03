using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class ProductDescription : Entity<Guid>
{
    public string Name { get; set; }

    public virtual Product Product { get; set; }
}
