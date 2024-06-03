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

namespace Application.Features.ProductReviews.Commands.Create;

public class CreateProductReviewCommand : IRequest<CreatedProductReviewResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string Content { get; set; }
    public required int Rating { get; set; }

    public string[] Roles => [Admin, Write, ProductReviewsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductReviews"];

    public class CreateProductReviewCommandHandler : IRequestHandler<CreateProductReviewCommand, CreatedProductReviewResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductReviewRepository _productReviewRepository;
        private readonly ProductReviewBusinessRules _productReviewBusinessRules;

        public CreateProductReviewCommandHandler(IMapper mapper, IProductReviewRepository productReviewRepository,
                                         ProductReviewBusinessRules productReviewBusinessRules)
        {
            _mapper = mapper;
            _productReviewRepository = productReviewRepository;
            _productReviewBusinessRules = productReviewBusinessRules;
        }

        public async Task<CreatedProductReviewResponse> Handle(CreateProductReviewCommand request, CancellationToken cancellationToken)
        {
            ProductReview productReview = _mapper.Map<ProductReview>(request);

            await _productReviewRepository.AddAsync(productReview);

            CreatedProductReviewResponse response = _mapper.Map<CreatedProductReviewResponse>(productReview);
            return response;
        }
    }
}