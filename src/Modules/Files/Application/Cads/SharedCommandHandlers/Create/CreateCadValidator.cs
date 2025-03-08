using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.Core.Common.TypedIds.Files;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Cads.SharedCommandHandlers.Create;

using static CadConstants;
using static Constants.FluentMessages;

public class CreateCadValidator : CommandValidator<CreateCadCommand, CadId>
{
    public CreateCadValidator()
    {
        RuleFor(r => r.Key)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(r => r.ContentType)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(r => r.Volume)
            .NotEmpty().WithMessage(RequiredError)
            .ExclusiveBetween(VolumeMin, VolumeMax).WithMessage(RangeError);

    }
}
