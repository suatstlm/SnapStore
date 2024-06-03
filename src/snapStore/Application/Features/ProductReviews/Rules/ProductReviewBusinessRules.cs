using Application.Features.ProductReviews.Constants;
using Application.Services.Repositories;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using NArchitecture.Core.Localization.Abstraction;
using Domain.Entities;

namespace Application.Features.ProductReviews.Rules;

public class ProductReviewBusinessRules : BaseBusinessRules
{
    private readonly IProductReviewRepository _productReviewRepository;
    private readonly ILocalizationService _localizationService;

    public ProductReviewBusinessRules(IProductReviewRepository productReviewRepository, ILocalizationService localizationService)
    {
        _productReviewRepository = productReviewRepository;
        _localizationService = localizationService;
    }

    private async Task throwBusinessException(string messageKey)
    {
        string message = await _localizationService.GetLocalizedAsync(messageKey, ProductReviewsBusinessMessages.SectionName);
        throw new BusinessException(message);
    }

    public async Task ProductReviewShouldExistWhenSelected(ProductReview? productReview)
    {
        if (productReview == null)
            await throwBusinessException(ProductReviewsBusinessMessages.ProductReviewNotExists);
    }

    public async Task ProductReviewIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        ProductReview? productReview = await _productReviewRepository.GetAsync(
            predicate: pr => pr.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await ProductReviewShouldExistWhenSelected(productReview);
    }
}