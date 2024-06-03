using FluentValidation;

namespace Application.Features.ProductDescriptions.Commands.Update;

public class UpdateProductDescriptionCommandValidator : AbstractValidator<UpdateProductDescriptionCommand>
{
    public UpdateProductDescriptionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
    }
}