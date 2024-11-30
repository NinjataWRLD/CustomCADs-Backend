using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Categories.Application.Categories.Commands.Create;

using static Constants.FluentMessages;

public class CreateCategoryCommandValidator : Validator<CreateCategoryCommand, CategoryId>
{
    public CreateCategoryCommandValidator()
    {
        RuleFor(x => x.Dto)
            .ChildRules(x => x
                .RuleFor(x => x.Name)
                    .NotEmpty().WithMessage(RequiredError)
        );
    }
}
