using NArchitecture.Core.Application.Responses;

namespace Application.Features.ProductReviews.Commands.Delete;

public class DeletedProductReviewResponse : IResponse
{
    public Guid Id { get; set; }
}