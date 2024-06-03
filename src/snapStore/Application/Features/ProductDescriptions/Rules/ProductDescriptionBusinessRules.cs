using Application.Features.ProductDescriptions.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.ProductDescriptions.Rules;

public class ProductDescriptionBusinessRules : BaseBusinessRules
{
    private readonly IProductDescriptionRepository _productDescriptionRepository;
    private readonly ILocalizationService _localizationService;

    public ProductDescriptionBusinessRules(IProductDescriptionRepository productDescriptionRepository, ILocalizationService localizationService)
    {
        _productDescriptionRepository = productDescriptionRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ProductDescriptionsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ProductDescriptionShouldExistWhenSelected(ProductDescription? productDescription)
    {
        if (productDescription == null)
            await throwBusinessException(ProductDescriptionsBusinessMessages.ProductDescriptionNotExists);
    }

    public async Task ProductDescriptionIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ProductDescription? productDescription = await _productDescriptionRepository.GetAsync(
            predicate: pd => pd.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ProductDescriptionShouldExistWhenSelected(productDescription);
    }
}