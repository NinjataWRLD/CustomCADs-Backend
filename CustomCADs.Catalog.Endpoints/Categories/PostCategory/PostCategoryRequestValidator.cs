using FastEndpoints;
using FluentValidation;
using static CustomCADs.Shared.Core.Constants;

namespace CustomCADs.Catalog.Endpoints.Categories.PostCategory;

public class PostCategoryRequestValidator : Validator<PostCategoryRequest>
{
    public PostCategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredErrorMessage);
    }
}
