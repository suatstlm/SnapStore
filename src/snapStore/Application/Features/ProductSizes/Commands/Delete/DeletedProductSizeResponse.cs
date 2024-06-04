using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductSizes.Commands.Delete;

public class DeletedProductSizeResponse : IResponse
{
    public Guid Id { get; set; }
}