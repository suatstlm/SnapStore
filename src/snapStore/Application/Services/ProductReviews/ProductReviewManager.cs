using Application.Features.ProductReviews.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductReviews;

public class ProductReviewManager : IProductReviewService
{
    private readonly IProductReviewRepository _productReviewRepository;
    private readonly ProductReviewBusinessRules _productReviewBusinessRules;

    public ProductReviewManager(IProductReviewRepository productReviewRepository, ProductReviewBusinessRules productReviewBusinessRules)
    {
        _productReviewRepository = productReviewRepository;
        _productReviewBusinessRules = productReviewBusinessRules;
    }

    public async Task<ProductReview?> GetAsync(
        Expression<Func<ProductReview, bool>> predicate,
        Func<IQueryable<ProductReview>, IIncludableQueryable<ProductReview, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ProductReview? productReview = await _productReviewRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return productReview;
    }

    public async Task<IPaginate<ProductReview>?> GetListAsync(
        Expression<Func<ProductReview, bool>>? predicate = null,
        Func<IQueryable<ProductReview>, IOrderedQueryable<ProductReview>>? orderBy = null,
        Func<IQueryable<ProductReview>, IIncludableQueryable<ProductReview, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ProductReview> productReviewList = await _productReviewRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return productReviewList;
    }

    public async Task<ProductReview> AddAsync(ProductReview productReview)
    {
        ProductReview addedProductReview = await _productReviewRepository.AddAsync(productReview);

        return addedProductReview;
    }

    public async Task<ProductReview> UpdateAsync(ProductReview productReview)
    {
        ProductReview updatedProductReview = await _productReviewRepository.UpdateAsync(productReview);

        return updatedProductReview;
    }

    public async Task<ProductReview> DeleteAsync(ProductReview productReview, bool permanent = false)
    {
        ProductReview deletedProductReview = await _productReviewRepository.DeleteAsync(productReview);

        return deletedProductReview;
    }
}
