using Application.Features.ProductDescriptions.Constants;
using Application.Features.ProductDescriptions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.ProductDescriptions.Constants.ProductDescriptionsOperationClaims;

namespace Application.Features.ProductDescriptions.Commands.Create;

public class CreateProductDescriptionCommand : IRequest<CreatedProductDescriptionResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public required string Name { get; set; }

    public string[] Roles => [Admin, Write, ProductDescriptionsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductDescriptions"];

    public class CreateProductDescriptionCommandHandler : IRequestHandler<CreateProductDescriptionCommand, CreatedProductDescriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductDescriptionRepository _productDescriptionRepository;
        private readonly ProductDescriptionBusinessRules _productDescriptionBusinessRules;

        public CreateProductDescriptionCommandHandler(IMapper mapper, IProductDescriptionRepository productDescriptionRepository,
                                         ProductDescriptionBusinessRules productDescriptionBusinessRules)
        {
            _mapper = mapper;
            _productDescriptionRepository = productDescriptionRepository;
            _productDescriptionBusinessRules = productDescriptionBusinessRules;
        }

        public async Task<CreatedProductDescriptionResponse> Handle(CreateProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            ProductDescription productDescription = _mapper.Map<ProductDescription>(request);

            await _productDescriptionRepository.AddAsync(productDescription);

            CreatedProductDescriptionResponse response = _mapper.Map<CreatedProductDescriptionResponse>(productDescription);
            return response;
        }
    }
}