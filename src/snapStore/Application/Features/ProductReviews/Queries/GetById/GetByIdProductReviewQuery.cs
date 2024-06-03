using Application.Features.ProductReviews.Constants;
using Application.Features.ProductReviews.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ProductReviews.Constants.ProductReviewsOperationClaims;

namespace Application.Features.ProductReviews.Queries.GetById;

public class GetByIdProductReviewQuery : IRequest<GetByIdProductReviewResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdProductReviewQueryHandler : IRequestHandler<GetByIdProductReviewQuery, GetByIdProductReviewResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductReviewRepository _productReviewRepository;
        private readonly ProductReviewBusinessRules _productReviewBusinessRules;

        public GetByIdProductReviewQueryHandler(IMapper mapper, IProductReviewRepository productReviewRepository, ProductReviewBusinessRules productReviewBusinessRules)
        {
            _mapper = mapper;
            _productReviewRepository = productReviewRepository;
            _productReviewBusinessRules = productReviewBusinessRules;
        }

        public async Task<GetByIdProductReviewResponse> Handle(GetByIdProductReviewQuery request, CancellationToken cancellationToken)
        {
            ProductReview? productReview = await _productReviewRepository.GetAsync(predicate: pr => pr.Id == request.Id, cancellationToken: cancellationToken);
            await _productReviewBusinessRules.ProductReviewShouldExistWhenSelected(productReview);

            GetByIdProductReviewResponse response = _mapper.Map<GetByIdProductReviewResponse>(productReview);
            return response;
        }
    }
}