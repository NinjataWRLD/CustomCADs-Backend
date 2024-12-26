using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Cads.SharedCommandHandlers.Create;

using static Constants.FluentMessages;

public class CreateCadValidator : Validator<CreateCadCommand, CadId>
{
    public CreateCadValidator()
    {
        RuleFor(r => r.Key)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(r => r.ContentType)
            .NotEmpty().WithMessage(RequiredError);

    }
}
