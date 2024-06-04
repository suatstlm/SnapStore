using Application.Features.ProductSizes.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.ProductSizes.Rules;

public class ProductSizeBusinessRules : BaseBusinessRules
{
    private readonly IProductSizeRepository _productSizeRepository;
    private readonly ILocalizationService _localizationService;

    public ProductSizeBusinessRules(IProductSizeRepository productSizeRepository, ILocalizationService localizationService)
    {
        _productSizeRepository = productSizeRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ProductSizesBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ProductSizeShouldExistWhenSelected(ProductSize? productSize)
    {
        if (productSize == null)
            await throwBusinessException(ProductSizesBusinessMessages.ProductSizeNotExists);
    }

    public async Task ProductSizeIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ProductSize? productSize = await _productSizeRepository.GetAsync(
            predicate: ps => ps.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ProductSizeShouldExistWhenSelected(productSize);
    }
}