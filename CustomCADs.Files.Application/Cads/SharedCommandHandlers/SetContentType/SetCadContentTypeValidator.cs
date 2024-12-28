using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetContentType;

using static Constants.FluentMessages;

public class SetCadContentTypeValidator : Validator<SetCadContentTypeCommand>
{
    public SetCadContentTypeValidator()
    {
        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);
    }
}
