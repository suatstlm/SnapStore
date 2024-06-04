using FluentValidation;

namespace Application.Features.ProductSizes.Commands.Delete;

public class DeleteProductSizeCommandValidator : AbstractValidator<DeleteProductSizeCommand>
{
    public DeleteProductSizeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}