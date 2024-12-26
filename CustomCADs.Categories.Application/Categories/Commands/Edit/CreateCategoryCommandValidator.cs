using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Categories.Application.Categories.Commands.Edit;

using static Constants.FluentMessages;

public class EditCategoryValidator : Validator<EditCategoryCommand>
{
    public EditCategoryValidator()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage(RequiredError);
    }
}
