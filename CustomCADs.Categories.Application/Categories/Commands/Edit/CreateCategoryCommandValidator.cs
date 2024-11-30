using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Categories.Application.Categories.Commands.Edit;

using static Constants.FluentMessages;

public class EditCategoryCommandValidator : Validator<EditCategoryCommand>
{
    public EditCategoryCommandValidator()
    {
        RuleFor(x => x.Dto)
            .ChildRules(x => x
                .RuleFor(x => x.Name)
                    .NotEmpty().WithMessage(RequiredError)
        );
    }
}
