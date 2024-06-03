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

namespace Application.Features.ProductDescriptions.Commands.Update;

public class UpdateProductDescriptionCommand : IRequest<UpdatedProductDescriptionResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }
    public required string Name { get; set; }

    public string[] Roles => [Admin, Write, ProductDescriptionsOperationClaims.Update];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductDescriptions"];

    public class UpdateProductDescriptionCommandHandler : IRequestHandler<UpdateProductDescriptionCommand, UpdatedProductDescriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductDescriptionRepository _productDescriptionRepository;
        private readonly ProductDescriptionBusinessRules _productDescriptionBusinessRules;

        public UpdateProductDescriptionCommandHandler(IMapper mapper, IProductDescriptionRepository productDescriptionRepository,
                                         ProductDescriptionBusinessRules productDescriptionBusinessRules)
        {
            _mapper = mapper;
            _productDescriptionRepository = productDescriptionRepository;
            _productDescriptionBusinessRules = productDescriptionBusinessRules;
        }

        public async Task<UpdatedProductDescriptionResponse> Handle(UpdateProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            ProductDescription? productDescription = await _productDescriptionRepository.GetAsync(predicate: pd => pd.Id == request.Id, cancellationToken: cancellationToken);
            await _productDescriptionBusinessRules.ProductDescriptionShouldExistWhenSelected(productDescription);
            productDescription = _mapper.Map(request, productDescription);

            await _productDescriptionRepository.UpdateAsync(productDescription!);

            UpdatedProductDescriptionResponse response = _mapper.Map<UpdatedProductDescriptionResponse>(productDescription);
            return response;
        }
    }
}