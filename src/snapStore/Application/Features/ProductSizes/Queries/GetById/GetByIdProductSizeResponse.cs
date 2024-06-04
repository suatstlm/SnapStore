using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductSizes.Queries.GetById;

public class GetByIdProductSizeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Size { get; set; }
    public int StockQuantity { get; set; }
}