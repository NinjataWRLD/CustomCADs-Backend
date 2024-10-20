using FastEndpoints;
using FluentValidation;
using static CustomCADs.Catalog.Domain.Shared.SharedConstants;

namespace CustomCADs.Catalog.Presentation.Categories.Endpoints.PostCategory;

public class PostCategoryRequestValidator : Validator<PostCategoryRequest>
{
    public PostCategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredErrorMessage);
    }
}
