using Application.Features.ProductReviews.Constants;
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

namespace Application.Features.ProductReviews.Commands.Delete;

public class DeleteProductReviewCommand : IRequest<DeletedProductReviewResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ProductReviewsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductReviews"];

    public class DeleteProductReviewCommandHandler : IRequestHandler<DeleteProductReviewCommand, DeletedProductReviewResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductReviewRepository _productReviewRepository;
        private readonly ProductReviewBusinessRules _productReviewBusinessRules;

        public DeleteProductReviewCommandHandler(IMapper mapper, IProductReviewRepository productReviewRepository,
                                         ProductReviewBusinessRules productReviewBusinessRules)
        {
            _mapper = mapper;
            _productReviewRepository = productReviewRepository;
            _productReviewBusinessRules = productReviewBusinessRules;
        }

        public async Task<DeletedProductReviewResponse> Handle(DeleteProductReviewCommand request, CancellationToken cancellationToken)
        {
            ProductReview? productReview = await _productReviewRepository.GetAsync(predicate: pr => pr.Id == request.Id, cancellationToken: cancellationToken);
            await _productReviewBusinessRules.ProductReviewShouldExistWhenSelected(productReview);

            await _productReviewRepository.DeleteAsync(productReview!);

            DeletedProductReviewResponse response = _mapper.Map<DeletedProductReviewResponse>(productReview);
            return response;
        }
    }
}