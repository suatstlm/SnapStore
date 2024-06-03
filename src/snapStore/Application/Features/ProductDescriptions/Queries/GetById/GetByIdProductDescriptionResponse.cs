using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductDescriptions.Queries.GetById;

public class GetByIdProductDescriptionResponse : IResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}