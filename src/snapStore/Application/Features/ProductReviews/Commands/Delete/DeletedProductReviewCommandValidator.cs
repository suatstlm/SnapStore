using FluentValidation;

namespace Application.Features.ProductReviews.Commands.Delete;

public class DeleteProductReviewCommandValidator : AbstractValidator<DeleteProductReviewCommand>
{
    public DeleteProductReviewCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}