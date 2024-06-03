using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductDescriptions;

public interface IProductDescriptionService
{
    Task<ProductDescription?> GetAsync(
        Expression<Func<ProductDescription, bool>> predicate,
        Func<IQueryable<ProductDescription>, IIncludableQueryable<ProductDescription, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ProductDescription>?> GetListAsync(
        Expression<Func<ProductDescription, bool>>? predicate = null,
        Func<IQueryable<ProductDescription>, IOrderedQueryable<ProductDescription>>? orderBy = null,
        Func<IQueryable<ProductDescription>, IIncludableQueryable<ProductDescription, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ProductDescription> AddAsync(ProductDescription productDescription);
    Task<ProductDescription> UpdateAsync(ProductDescription productDescription);
    Task<ProductDescription> DeleteAsync(ProductDescription productDescription, bool permanent = false);
}
