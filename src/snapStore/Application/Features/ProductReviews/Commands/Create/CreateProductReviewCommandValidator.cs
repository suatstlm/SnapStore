using FluentValidation;

namespace Application.Features.ProductReviews.Commands.Create;

public class CreateProductReviewCommandValidator : AbstractValidator<CreateProductReviewCommand>
{
    public CreateProductReviewCommandValidator()
    {
        RuleFor(c => c.Content).NotEmpty();
        RuleFor(c => c.Rating).NotEmpty();
    }
}