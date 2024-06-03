using Application.Features.ProductDescriptions.Constants;
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

namespace Application.Features.ProductDescriptions.Commands.Delete;

public class DeleteProductDescriptionCommand : IRequest<DeletedProductDescriptionResponse>, ISecuredRequest, ICacheRemoverRequest, ILoggableRequest, ITransactionalRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Write, ProductDescriptionsOperationClaims.Delete];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetProductDescriptions"];

    public class DeleteProductDescriptionCommandHandler : IRequestHandler<DeleteProductDescriptionCommand, DeletedProductDescriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductDescriptionRepository _productDescriptionRepository;
        private readonly ProductDescriptionBusinessRules _productDescriptionBusinessRules;

        public DeleteProductDescriptionCommandHandler(IMapper mapper, IProductDescriptionRepository productDescriptionRepository,
                                         ProductDescriptionBusinessRules productDescriptionBusinessRules)
        {
            _mapper = mapper;
            _productDescriptionRepository = productDescriptionRepository;
            _productDescriptionBusinessRules = productDescriptionBusinessRules;
        }

        public async Task<DeletedProductDescriptionResponse> Handle(DeleteProductDescriptionCommand request, CancellationToken cancellationToken)
        {
            ProductDescription? productDescription = await _productDescriptionRepository.GetAsync(predicate: pd => pd.Id == request.Id, cancellationToken: cancellationToken);
            await _productDescriptionBusinessRules.ProductDescriptionShouldExistWhenSelected(productDescription);

            await _productDescriptionRepository.DeleteAsync(productDescription!);

            DeletedProductDescriptionResponse response = _mapper.Map<DeletedProductDescriptionResponse>(productDescription);
            return response;
        }
    }
}