using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductReviews.Queries.GetById;

public class GetByIdProductReviewResponse : IResponse
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
}