using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Categories.Application.Categories.Commands.Create;

using static Constants.FluentMessages;

public class CreateCategoryValidator : Validator<CreateCategoryCommand, CategoryId>
{
    public CreateCategoryValidator()
    {
        RuleFor(x => x.Dto.Name)
            .NotEmpty().WithMessage(RequiredError);
    }
}
