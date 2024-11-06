using FluentValidation;

namespace CustomCADs.Catalog.Endpoints.Categories.PostCategory;

using static Constants;

public class PostCategoryRequestValidator : Validator<PostCategoryRequest>
{
    public PostCategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredErrorMessage);
    }
}
