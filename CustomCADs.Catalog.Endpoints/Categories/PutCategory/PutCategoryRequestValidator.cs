using FastEndpoints;
using FluentValidation;
using static CustomCADs.Shared.Core.Constants;

namespace CustomCADs.Catalog.Endpoints.Categories.PutCategory;

public class PutCategoryRequestValidator : Validator<PutCategoryRequest>
{
    public PutCategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredErrorMessage);
    }
}
