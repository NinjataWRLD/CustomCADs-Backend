using CustomCADs.Catalog.Domain.Products;
using FluentValidation;

namespace CustomCADs.Catalog.Endpoints.Products.Put;

using static Constants.FluentMessages;
using static ProductConstants;

public class PostProductRequestValidator : Validator<PutProductRequest>
{
    public PostProductRequestValidator()
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
