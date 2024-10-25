using FastEndpoints;
using FluentValidation;
using static CustomCADs.Shared.Domain.Constants;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints.PutCategory;

public class PutCategoryRequestValidator : Validator<PutCategoryRequest>
{
    public PutCategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredErrorMessage);
    }
}
