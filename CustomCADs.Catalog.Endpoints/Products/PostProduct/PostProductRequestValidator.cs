using CustomCADs.Catalog.Domain.Products;
using FluentValidation;

namespace CustomCADs.Catalog.Endpoints.Products.PostProduct;

using static Constants;
using static ProductConstants;

public class PostProductRequestValidator : Validator<PostProductRequest>
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

        RuleFor(r => r.Cost)
            .ExclusiveBetween(CostMin, CostMax).WithMessage(RangeErrorMessage);

        RuleFor(r => r.Image)
            .NotNull().WithMessage(RequiredErrorMessage);

        RuleFor(r => r.File)
            .NotNull().WithMessage(RequiredErrorMessage);
    }
}
