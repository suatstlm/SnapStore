using NArchitecture.Core.Application.Dtos;

namespace Application.Features.ProductDescriptions.Queries.GetList;

public class GetListProductDescriptionListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}