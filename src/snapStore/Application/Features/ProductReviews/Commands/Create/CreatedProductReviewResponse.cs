using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductReviews.Commands.Create;

public class CreatedProductReviewResponse : IResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
}