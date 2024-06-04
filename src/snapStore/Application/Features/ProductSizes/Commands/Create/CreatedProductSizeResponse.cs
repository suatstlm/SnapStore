using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductSizes.Commands.Create;

public class CreatedProductSizeResponse : IResponse
{
    public Guid Id { get; set; }
    public string Size { get; set; }
    public int StockQuantity { get; set; }
}