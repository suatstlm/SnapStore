using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductDescriptions.Commands.Update;

public class UpdatedProductDescriptionResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}