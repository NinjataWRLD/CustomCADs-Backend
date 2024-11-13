using CustomCADs.Catalog.Domain.Products.Validation;
using FluentValidation;

namespace CustomCADs.Catalog.Endpoints.Products.PostProduct;

using static Constants.FluentMessages;
using static ProductConstants;

public class PostProductRequestValidator : Validator<PostProductRequest>
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

        RuleFor(r => r.Price).ChildRules(v =>
        {
            v.RuleFor(c => c.Amount)
                .ExclusiveBetween(CostMin, CostMax).WithMessage(RangeError);
        });

        RuleFor(r => r.Image)
            .NotNull().WithMessage(RequiredError);

        RuleFor(r => r.File)
            .NotNull().WithMessage(RequiredError);
    }
}
