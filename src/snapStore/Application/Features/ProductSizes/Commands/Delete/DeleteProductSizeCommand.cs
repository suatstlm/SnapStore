using Application.Features.ProductSizes.Constants;
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

namespace Application.Features.ProductSizes.Commands.Delete;

public class DeleteProductSizeCommand : IRequest<DeletedProductSizeResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ProductSizesOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductSizes"];

    public class DeleteProductSizeCommandHandler : IRequestHandler<DeleteProductSizeCommand, DeletedProductSizeResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductSizeRepository _productSizeRepository;
        private readonly ProductSizeBusinessRules _productSizeBusinessRules;

        public DeleteProductSizeCommandHandler(IMapper mapper, IProductSizeRepository productSizeRepository,
                                         ProductSizeBusinessRules productSizeBusinessRules)
        {
            _mapper = mapper;
            _productSizeRepository = productSizeRepository;
            _productSizeBusinessRules = productSizeBusinessRules;
        }

        public async Task<DeletedProductSizeResponse> Handle(DeleteProductSizeCommand request, CancellationToken cancellationToken)
        {
            ProductSize? productSize = await _productSizeRepository.GetAsync(predicate: ps => ps.Id == request.Id, cancellationToken: cancellationToken);
            await _productSizeBusinessRules.ProductSizeShouldExistWhenSelected(productSize);

            await _productSizeRepository.DeleteAsync(productSize!);

            DeletedProductSizeResponse response = _mapper.Map<DeletedProductSizeResponse>(productSize);
            return response;
        }
    }
}