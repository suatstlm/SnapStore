using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductDescriptionRepository : EfRepositoryBase<ProductDescription, Guid, BaseDbContext>, IProductDescriptionRepository
{
    public ProductDescriptionRepository(BaseDbContext context) : base(context)
    {
    }
}