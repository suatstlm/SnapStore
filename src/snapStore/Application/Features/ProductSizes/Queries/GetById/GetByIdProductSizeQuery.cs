using Application.Features.ProductSizes.Constants;
using Application.Features.ProductSizes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ProductSizes.Constants.ProductSizesOperationClaims;

namespace Application.Features.ProductSizes.Queries.GetById;

public class GetByIdProductSizeQuery : IRequest<GetByIdProductSizeResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdProductSizeQueryHandler : IRequestHandler<GetByIdProductSizeQuery, GetByIdProductSizeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly ProductSizeBusinessRules _productSizeBusinessRules;

        public GetByIdProductSizeQueryHandler(IMapper mapper, IProductSizeRepository productSizeRepository, ProductSizeBusinessRules productSizeBusinessRules)
        {
            _mapper = mapper;
            _productSizeRepository = productSizeRepository;
            _productSizeBusinessRules = productSizeBusinessRules;
        }

        public async Task<GetByIdProductSizeResponse> Handle(GetByIdProductSizeQuery request, CancellationToken cancellationToken)
        {
            ProductSize? productSize = await _productSizeRepository.GetAsync(predicate: ps => ps.Id == request.Id, cancellationToken: cancellationToken);
            await _productSizeBusinessRules.ProductSizeShouldExistWhenSelected(productSize);

            GetByIdProductSizeResponse response = _mapper.Map<GetByIdProductSizeResponse>(productSize);
            return response;
        }
    }
}