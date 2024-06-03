using Application.Features.ProductDescriptions.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.ProductDescriptions;

public class ProductDescriptionManager : IProductDescriptionService
{
    private readonly IProductDescriptionRepository _productDescriptionRepository;
    private readonly ProductDescriptionBusinessRules _productDescriptionBusinessRules;

    public ProductDescriptionManager(IProductDescriptionRepository productDescriptionRepository, ProductDescriptionBusinessRules productDescriptionBusinessRules)
    {
        _productDescriptionRepository = productDescriptionRepository;
        _productDescriptionBusinessRules = productDescriptionBusinessRules;
    }

    public async Task<ProductDescription?> GetAsync(
        Expression<Func<ProductDescription, bool>> predicate,
        Func<IQueryable<ProductDescription>, IIncludableQueryable<ProductDescription, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        ProductDescription? productDescription = await _productDescriptionRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return productDescription;
    }

    public async Task<IPaginate<ProductDescription>?> GetListAsync(
        Expression<Func<ProductDescription, bool>>? predicate = null,
        Func<IQueryable<ProductDescription>, IOrderedQueryable<ProductDescription>>? orderBy = null,
        Func<IQueryable<ProductDescription>, IIncludableQueryable<ProductDescription, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<ProductDescription> productDescriptionList = await _productDescriptionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return productDescriptionList;
    }

    public async Task<ProductDescription> AddAsync(ProductDescription productDescription)
    {
        ProductDescription addedProductDescription = await _productDescriptionRepository.AddAsync(productDescription);

        return addedProductDescription;
    }

    public async Task<ProductDescription> UpdateAsync(ProductDescription productDescription)
    {
        ProductDescription updatedProductDescription = await _productDescriptionRepository.UpdateAsync(productDescription);

        return updatedProductDescription;
    }

    public async Task<ProductDescription> DeleteAsync(ProductDescription productDescription, bool permanent = false)
    {
        ProductDescription deletedProductDescription = await _productDescriptionRepository.DeleteAsync(productDescription);

        return deletedProductDescription;
    }
}
