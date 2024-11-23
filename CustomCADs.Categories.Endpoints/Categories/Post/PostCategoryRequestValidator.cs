using FluentValidation;

namespace CustomCADs.Categories.Endpoints.Categories.Post;

using static Constants.FluentMessages;

public class PostCategoryRequestValidator : Validator<PostCategoryRequest>
{
    public PostCategoryRequestValidator()
    {
        RuleFor(r => r.Name)
            .NotEmpty().WithMessage(RequiredError);
    }
}
