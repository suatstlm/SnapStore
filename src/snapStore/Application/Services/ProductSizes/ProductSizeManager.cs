using Application.Features.ProductSizes.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductSizes;

public class ProductSizeManager : IProductSizeService
{
    private readonly IProductSizeRepository _productSizeRepository;
    private readonly ProductSizeBusinessRules _productSizeBusinessRules;

    public ProductSizeManager(IProductSizeRepository productSizeRepository, ProductSizeBusinessRules productSizeBusinessRules)
    {
        _productSizeRepository = productSizeRepository;
        _productSizeBusinessRules = productSizeBusinessRules;
    }

    public async Task<ProductSize?> GetAsync(
        Expression<Func<ProductSize, bool>> predicate,
        Func<IQueryable<ProductSize>, IIncludableQueryable<ProductSize, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ProductSize? productSize = await _productSizeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return productSize;
    }

    public async Task<IPaginate<ProductSize>?> GetListAsync(
        Expression<Func<ProductSize, bool>>? predicate = null,
        Func<IQueryable<ProductSize>, IOrderedQueryable<ProductSize>>? orderBy = null,
        Func<IQueryable<ProductSize>, IIncludableQueryable<ProductSize, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ProductSize> productSizeList = await _productSizeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return productSizeList;
    }

    public async Task<ProductSize> AddAsync(ProductSize productSize)
    {
        ProductSize addedProductSize = await _productSizeRepository.AddAsync(productSize);

        return addedProductSize;
    }

    public async Task<ProductSize> UpdateAsync(ProductSize productSize)
    {
        ProductSize updatedProductSize = await _productSizeRepository.UpdateAsync(productSize);

        return updatedProductSize;
    }

    public async Task<ProductSize> DeleteAsync(ProductSize productSize, bool permanent = false)
    {
        ProductSize deletedProductSize = await _productSizeRepository.DeleteAsync(productSize);

        return deletedProductSize;
    }
}
