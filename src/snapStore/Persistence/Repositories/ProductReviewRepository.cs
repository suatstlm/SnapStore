using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductReviewRepository : EfRepositoryBase<ProductReview, Guid, BaseDbContext>, IProductReviewRepository
{
    public ProductReviewRepository(BaseDbContext context) : base(context)
    {
    }
}