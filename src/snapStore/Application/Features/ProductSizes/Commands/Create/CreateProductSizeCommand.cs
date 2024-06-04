using Application.Features.ProductSizes.Constants;
using Application.Features.ProductSizes.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ProductSizes.Constants.ProductSizesOperationClaims;

namespace Application.Features.ProductSizes.Commands.Create;

public class CreateProductSizeCommand : IRequest<CreatedProductSizeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string Size { get; set; }
    public required int StockQuantity { get; set; }

    public string[] Roles => [Admin, Write, ProductSizesOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductSizes"];

    public class CreateProductSizeCommandHandler : IRequestHandler<CreateProductSizeCommand, CreatedProductSizeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly ProductSizeBusinessRules _productSizeBusinessRules;

        public CreateProductSizeCommandHandler(IMapper mapper, IProductSizeRepository productSizeRepository,
                                         ProductSizeBusinessRules productSizeBusinessRules)
        {
            _mapper = mapper;
            _productSizeRepository = productSizeRepository;
            _productSizeBusinessRules = productSizeBusinessRules;
        }

        public async Task<CreatedProductSizeResponse> Handle(CreateProductSizeCommand request, CancellationToken cancellationToken)
        {
            ProductSize productSize = _mapper.Map<ProductSize>(request);

            await _productSizeRepository.AddAsync(productSize);

            CreatedProductSizeResponse response = _mapper.Map<CreatedProductSizeResponse>(productSize);
            return response;
        }
    }
}