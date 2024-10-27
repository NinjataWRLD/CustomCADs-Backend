using FastEndpoints;
using FluentValidation;
using static CustomCADs.Shared.Domain.Constants;

namespace CustomCADs.Catalog.Endpoints.Categories.Endpoints.PostCategory;

public class PostCategoryRequestValidator : Validator<PostCategoryRequest>
{
    public PostCategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredErrorMessage);
    }
}
