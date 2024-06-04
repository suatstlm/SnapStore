using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class ProductSizeRepository : EfRepositoryBase<ProductSize, Guid, BaseDbContext>, IProductSizeRepository
{
    public ProductSizeRepository(BaseDbContext context) : base(context)
    {
    }
}