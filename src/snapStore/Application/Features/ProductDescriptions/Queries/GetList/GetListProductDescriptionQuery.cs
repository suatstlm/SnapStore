using Application.Features.ProductDescriptions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using NArchitecture.Core.Persistence.Paging;
using MediatR;
using static Application.Features.ProductDescriptions.Constants.ProductDescriptionsOperationClaims;

namespace Application.Features.ProductDescriptions.Queries.GetList;

public class GetListProductDescriptionQuery : IRequest<GetListResponse<GetListProductDescriptionListItemDto>>, ISecuredRequest, ICachableRequest
{
    public PageRequest PageRequest { get; set; }

    public string[] Roles => [Admin, Read];

    public bool BypassCache { get; }
    public string? CacheKey => $"GetListProductDescriptions({PageRequest.PageIndex},{PageRequest.PageSize})";
    public string? CacheGroupKey => "GetProductDescriptions";
    public TimeSpan? SlidingExpiration { get; }

    public class GetListProductDescriptionQueryHandler : IRequestHandler<GetListProductDescriptionQuery, GetListResponse<GetListProductDescriptionListItemDto>>
    {
        private readonly IProductDescriptionRepository _productDescriptionRepository;
        private readonly IMapper _mapper;

        public GetListProductDescriptionQueryHandler(IProductDescriptionRepository productDescriptionRepository, IMapper mapper)
        {
            _productDescriptionRepository = productDescriptionRepository;
            _mapper = mapper;
        }

        public async Task<GetListResponse<GetListProductDescriptionListItemDto>> Handle(GetListProductDescriptionQuery request, CancellationToken cancellationToken)
        {
            IPaginate<ProductDescription> productDescriptions = await _productDescriptionRepository.GetListAsync(
                index: request.PageRequest.PageIndex,
                size: request.PageRequest.PageSize, 
                cancellationToken: cancellationToken
            );

            GetListResponse<GetListProductDescriptionListItemDto> response = _mapper.Map<GetListResponse<GetListProductDescriptionListItemDto>>(productDescriptions);
            return response;
        }
    }
}