using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductDescriptions.Commands.Delete;

public class DeletedProductDescriptionResponse : IResponse
{
    public Guid Id { get; set; }
}