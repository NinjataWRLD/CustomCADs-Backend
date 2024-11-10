using CustomCADs.Catalog.Domain.Products;
using FluentValidation;

namespace CustomCADs.Catalog.Endpoints.Products.PutProduct;

using static Constants.Errors;
using static ProductConstants;

public class PostProductRequestValidator : Validator<PutProductRequest>
{
    public PostProductRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredErrorMessage)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthErrorMessage);

        RuleFor(r => r.Description)
            .NotEmpty().WithMessage(RequiredErrorMessage)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthErrorMessage);

        RuleFor(r => r.CategoryId)
            .NotEmpty().WithMessage(RequiredErrorMessage);

        RuleFor(r => r.Price).ChildRules(v =>
        {
            v.RuleFor(c => c.Amount)
                .ExclusiveBetween(CostMin, CostMax).WithMessage(RangeErrorMessage);
        });
    }
}
