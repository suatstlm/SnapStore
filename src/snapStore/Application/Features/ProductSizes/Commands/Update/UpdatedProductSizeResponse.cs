using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductSizes.Commands.Update;

public class UpdatedProductSizeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Size { get; set; }
    public int StockQuantity { get; set; }
}