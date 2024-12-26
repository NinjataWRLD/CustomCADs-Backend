using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.UseCases.Images.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Images.SharedCommandHandlers.SetKey;

using static Constants.FluentMessages;

public class SetImageKeyValidator : Validator<SetImageKeyCommand>
{
    public SetImageKeyValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty().WithMessage(RequiredError);
    }
}
