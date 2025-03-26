using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.SetVolume;

using static CadConstants;
using static Constants.FluentMessages;

public class SetCadVolumeValidator : CommandValidator<SetCadVolumeCommand>
{
    public SetCadVolumeValidator()
    {
        RuleFor(x => x.Volume)
            .NotEmpty().WithMessage(RequiredError)
            .ExclusiveBetween(VolumeMin, VolumeMax).WithMessage(RangeError);
    }
}
