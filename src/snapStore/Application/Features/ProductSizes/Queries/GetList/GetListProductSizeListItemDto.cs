using NArchitecture.Core.Application.Dtos;

namespace Application.Features.ProductSizes.Queries.GetList;

public class GetListProductSizeListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Size { get; set; }
    public int StockQuantity { get; set; }
}