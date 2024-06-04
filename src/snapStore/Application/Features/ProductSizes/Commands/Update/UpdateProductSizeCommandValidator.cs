using FluentValidation;

namespace Application.Features.ProductSizes.Commands.Update;

public class UpdateProductSizeCommandValidator : AbstractValidator<UpdateProductSizeCommand>
{
    public UpdateProductSizeCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Size).NotEmpty();
        RuleFor(c => c.StockQuantity).NotEmpty();
    }
}