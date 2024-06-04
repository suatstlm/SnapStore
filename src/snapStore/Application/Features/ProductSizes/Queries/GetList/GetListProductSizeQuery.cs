using Application.Features.ProductSizes.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.ProductSizes.Constants.ProductSizesOperationClaims;

namespace Application.Features.ProductSizes.Queries.GetList;

public class GetListProductSizeQuery : IRequest<GetListResponse<GetListProductSizeListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListProductSizes({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetProductSizes";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListProductSizeQueryHandler : IRequestHandler<GetListProductSizeQuery, GetListResponse<GetListProductSizeListItemDto>>
    {
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly IMapper _mapper;

        public GetListProductSizeQueryHandler(IProductSizeRepository productSizeRepository, IMapper mapper)
        {
            _productSizeRepository = productSizeRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProductSizeListItemDto>> Handle(GetListProductSizeQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProductSize> productSizes = await _productSizeRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListProductSizeListItemDto> response = _mapper.Map<GetListResponse<GetListProductSizeListItemDto>>(productSizes);
            return response;
        }
    }
}