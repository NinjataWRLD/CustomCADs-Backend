using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Cads.SharedCommandHandlers.SetCoords;

using static Constants.FluentMessages;

public class SetCadCoordsValidator : Validator<SetCadCoordsCommand>
{
    public SetCadCoordsValidator()
    {
        When(x => x.CamCoordinates != null, () =>
        {
            RuleFor(x => x.CamCoordinates!.X)
                .NotEmpty().WithMessage(RequiredError);

            RuleFor(x => x.CamCoordinates!.Y)
                .NotEmpty().WithMessage(RequiredError);

            RuleFor(x => x.CamCoordinates!.Z)
                .NotEmpty().WithMessage(RequiredError);
        });

        When(x => x.PanCoordinates != null, () =>
        {
            RuleFor(x => x.PanCoordinates!.X)
                .NotEmpty().WithMessage(RequiredError);

            RuleFor(x => x.PanCoordinates!.Y)
                .NotEmpty().WithMessage(RequiredError);

            RuleFor(x => x.PanCoordinates!.Z)
                .NotEmpty().WithMessage(RequiredError);
        });
    }
}
