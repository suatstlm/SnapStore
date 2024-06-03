using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductDescriptions.Commands.Create;

public class CreatedProductDescriptionResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}