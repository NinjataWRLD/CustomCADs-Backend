using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.SetContentType;

using static Constants.FluentMessages;

public class SetCadContentTypeValidator : CommandValidator<SetCadContentTypeCommand>
{
    public SetCadContentTypeValidator()
    {
        RuleFor(x => x.ContentType)
            .NotEmpty().WithMessage(RequiredError);
    }
}
