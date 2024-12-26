using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetKey;

using static Constants.FluentMessages;

public class SetCadKeyValidator : Validator<SetCadKeyCommand>
{
    public SetCadKeyValidator()
    {
        RuleFor(x => x.Key)
            .NotEmpty().WithMessage(RequiredError);
    }
}
