using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductReviews;

public interface IProductReviewService
{
    Task<ProductReview?> GetAsync(
        Expression<Func<ProductReview, bool>> predicate,
        Func<IQueryable<ProductReview>, IIncludableQueryable<ProductReview, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<ProductReview>?> GetListAsync(
        Expression<Func<ProductReview, bool>>? predicate = null,
        Func<IQueryable<ProductReview>, IOrderedQueryable<ProductReview>>? orderBy = null,
        Func<IQueryable<ProductReview>, IIncludableQueryable<ProductReview, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<ProductReview> AddAsync(ProductReview productReview);
    Task<ProductReview> UpdateAsync(ProductReview productReview);
    Task<ProductReview> DeleteAsync(ProductReview productReview, bool permanent = false);
}
