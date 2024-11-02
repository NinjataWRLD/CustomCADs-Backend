using FastEndpoints;
using FluentValidation;
using static CustomCADs.Catalog.Domain.Products.ProductConstants;
using static CustomCADs.Shared.Core.Constants;

namespace CustomCADs.Catalog.Endpoints.Products.PutProduct;

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

        RuleFor(r => r.Cost)
            .ExclusiveBetween(CostMin, CostMax).WithMessage(RangeErrorMessage);
    }
}
