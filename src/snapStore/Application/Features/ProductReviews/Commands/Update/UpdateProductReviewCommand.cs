using Application.Features.ProductReviews.Constants;
using Application.Features.ProductReviews.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ProductReviews.Constants.ProductReviewsOperationClaims;

namespace Application.Features.ProductReviews.Commands.Update;

public class UpdateProductReviewCommand : IRequest<UpdatedProductReviewResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Content { get; set; }
    public required int Rating { get; set; }

    public string[] Roles => [Admin, Write, ProductReviewsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductReviews"];

    public class UpdateProductReviewCommandHandler : IRequestHandler<UpdateProductReviewCommand, UpdatedProductReviewResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductReviewRepository _productReviewRepository;
        private readonly ProductReviewBusinessRules _productReviewBusinessRules;

        public UpdateProductReviewCommandHandler(IMapper mapper, IProductReviewRepository productReviewRepository,
                                         ProductReviewBusinessRules productReviewBusinessRules)
        {
            _mapper = mapper;
            _productReviewRepository = productReviewRepository;
            _productReviewBusinessRules = productReviewBusinessRules;
        }

        public async Task<UpdatedProductReviewResponse> Handle(UpdateProductReviewCommand request, CancellationToken cancellationToken)
        {
            ProductReview? productReview = await _productReviewRepository.GetAsync(predicate: pr => pr.Id == request.Id, cancellationToken: cancellationToken);
            await _productReviewBusinessRules.ProductReviewShouldExistWhenSelected(productReview);
            productReview = _mapper.Map(request, productReview);

            await _productReviewRepository.UpdateAsync(productReview!);

            UpdatedProductReviewResponse response = _mapper.Map<UpdatedProductReviewResponse>(productReview);
            return response;
        }
    }
}