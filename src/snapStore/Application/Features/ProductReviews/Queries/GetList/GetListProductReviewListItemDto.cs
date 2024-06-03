using NArchitecture.Core.Application.Dtos;

namespace Application.Features.ProductReviews.Queries.GetList;

public class GetListProductReviewListItemDto : IDto
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int Rating { get; set; }
}