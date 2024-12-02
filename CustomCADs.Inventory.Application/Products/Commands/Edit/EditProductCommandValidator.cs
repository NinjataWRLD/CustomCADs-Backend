using CustomCADs.Inventory.Domain.Products;
using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Inventory.Application.Products.Commands.Edit;

using static Constants.FluentMessages;
using static ProductConstants;

public class EditProductCommandValidator : Validator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);

        RuleFor(r => r.CategoryId)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(r => r.Price)
            .ExclusiveBetween(CostMin, CostMax).WithMessage(RangeError);
    }
}
