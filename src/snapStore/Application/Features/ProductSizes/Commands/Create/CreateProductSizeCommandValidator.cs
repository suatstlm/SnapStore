using FluentValidation;

namespace Application.Features.ProductSizes.Commands.Create;

public class CreateProductSizeCommandValidator : AbstractValidator<CreateProductSizeCommand>
{
    public CreateProductSizeCommandValidator()
    {
        RuleFor(c => c.Size).NotEmpty();
        RuleFor(c => c.StockQuantity).NotEmpty();
    }
}