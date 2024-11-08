using FluentValidation;

namespace CustomCADs.Catalog.Endpoints.Categories.PutCategory;

using static Constants.Errors;

public class PutCategoryRequestValidator : Validator<PutCategoryRequest>
{
    public PutCategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredErrorMessage);
    }
}
