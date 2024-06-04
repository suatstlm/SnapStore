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

namespace Application.Features.ProductSizes.Commands.Update;

public class UpdateProductSizeCommand : IRequest<UpdatedProductSizeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Size { get; set; }
    public required int StockQuantity { get; set; }

    public string[] Roles => [Admin, Write, ProductSizesOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductSizes"];

    public class UpdateProductSizeCommandHandler : IRequestHandler<UpdateProductSizeCommand, UpdatedProductSizeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly ProductSizeBusinessRules _productSizeBusinessRules;

        public UpdateProductSizeCommandHandler(IMapper mapper, IProductSizeRepository productSizeRepository,
                                         ProductSizeBusinessRules productSizeBusinessRules)
        {
            _mapper = mapper;
            _productSizeRepository = productSizeRepository;
            _productSizeBusinessRules = productSizeBusinessRules;
        }

        public async Task<UpdatedProductSizeResponse> Handle(UpdateProductSizeCommand request, CancellationToken cancellationToken)
        {
            ProductSize? productSize = await _productSizeRepository.GetAsync(predicate: ps => ps.Id == request.Id, cancellationToken: cancellationToken);
            await _productSizeBusinessRules.ProductSizeShouldExistWhenSelected(productSize);
            productSize = _mapper.Map(request, productSize);

            await _productSizeRepository.UpdateAsync(productSize!);

            UpdatedProductSizeResponse response = _mapper.Map<UpdatedProductSizeResponse>(productSize);
            return response;
        }
    }
}