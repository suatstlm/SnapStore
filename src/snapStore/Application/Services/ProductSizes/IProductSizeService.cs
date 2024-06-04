using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductSizes;

public interface IProductSizeService
{
    Task<ProductSize?> GetAsync(
        Expression<Func<ProductSize, bool>> predicate,
        Func<IQueryable<ProductSize>, IIncludableQueryable<ProductSize, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ProductSize>?> GetListAsync(
        Expression<Func<ProductSize, bool>>? predicate = null,
        Func<IQueryable<ProductSize>, IOrderedQueryable<ProductSize>>? orderBy = null,
        Func<IQueryable<ProductSize>, IIncludableQueryable<ProductSize, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ProductSize> AddAsync(ProductSize productSize);
    Task<ProductSize> UpdateAsync(ProductSize productSize);
    Task<ProductSize> DeleteAsync(ProductSize productSize, bool permanent = false);
}
