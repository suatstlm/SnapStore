using FluentValidation;

namespace Application.Features.ProductDescriptions.Commands.Delete;

public class DeleteProductDescriptionCommandValidator : AbstractValidator<DeleteProductDescriptionCommand>
{
    public DeleteProductDescriptionCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}