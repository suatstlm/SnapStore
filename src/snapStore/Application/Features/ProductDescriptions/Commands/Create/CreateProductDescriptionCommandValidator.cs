using FluentValidation;

namespace Application.Features.ProductDescriptions.Commands.Create;

public class CreateProductDescriptionCommandValidator : AbstractValidator<CreateProductDescriptionCommand>
{
    public CreateProductDescriptionCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
    }
}