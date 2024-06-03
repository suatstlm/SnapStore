using Application.Features.ProductReviews.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.ProductReviews.Constants.ProductReviewsOperationClaims;

namespace Application.Features.ProductReviews.Queries.GetList;

public class GetListProductReviewQuery : IRequest<GetListResponse<GetListProductReviewListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListProductReviews({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetProductReviews";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListProductReviewQueryHandler : IRequestHandler<GetListProductReviewQuery, GetListResponse<GetListProductReviewListItemDto>>
    {
        private readonly IProductReviewRepository _productReviewRepository;
        private readonly IMapper _mapper;

        public GetListProductReviewQueryHandler(IProductReviewRepository productReviewRepository, IMapper mapper)
        {
            _productReviewRepository = productReviewRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProductReviewListItemDto>> Handle(GetListProductReviewQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProductReview> productReviews = await _productReviewRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListProductReviewListItemDto> response = _mapper.Map<GetListResponse<GetListProductReviewListItemDto>>(productReviews);
            return response;
        }
    }
}