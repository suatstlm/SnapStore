using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities;

public class ProductReview : Entity<Guid>
{
    public string Content { get; set; }
    public int Rating { get; set; }

    public virtual User User { get; set; }
    public virtual Product Product { get; set; }
}