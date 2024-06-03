using Application.Features.ProductDescriptions.Constants;
using Application.Features.ProductDescriptions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.ProductDescriptions.Constants.ProductDescriptionsOperationClaims;

namespace Application.Features.ProductDescriptions.Queries.GetById;

public class GetByIdProductDescriptionQuery : IRequest<GetByIdProductDescriptionResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdProductDescriptionQueryHandler : IRequestHandler<GetByIdProductDescriptionQuery, GetByIdProductDescriptionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IProductDescriptionRepository _productDescriptionRepository;
        private readonly ProductDescriptionBusinessRules _productDescriptionBusinessRules;

        public GetByIdProductDescriptionQueryHandler(IMapper mapper, IProductDescriptionRepository productDescriptionRepository, ProductDescriptionBusinessRules productDescriptionBusinessRules)
        {
            _mapper = mapper;
            _productDescriptionRepository = productDescriptionRepository;
            _productDescriptionBusinessRules = productDescriptionBusinessRules;
        }

        public async Task<GetByIdProductDescriptionResponse> Handle(GetByIdProductDescriptionQuery request, CancellationToken cancellationToken)
        {
            ProductDescription? productDescription = await _productDescriptionRepository.GetAsync(predicate: pd => pd.Id == request.Id, cancellationToken: cancellationToken);
            await _productDescriptionBusinessRules.ProductDescriptionShouldExistWhenSelected(productDescription);

            GetByIdProductDescriptionResponse response = _mapper.Map<GetByIdProductDescriptionResponse>(productDescription);
            return response;
        }
    }
}