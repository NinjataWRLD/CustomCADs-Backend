using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Catalog.Application.Products.Commands.Create;

using static Constants.FluentMessages;
using static ProductConstants;

public class CreateProductValidator : Validator<CreateProductCommand, ProductId>
{
    public CreateProductValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);

        RuleFor(r => r.Price)
            .ExclusiveBetween(CostMin, CostMax).WithMessage(RangeError);
    }
}
