using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.UseCases.Cads.Commands;
using FluentValidation;

namespace CustomCADs.Files.Application.Cads.Commands.Shared.SetCoords;

using static CadConstants.Coordinates;
using static Constants.FluentMessages;

public class SetCadCoordsValidator : CommandValidator<SetCadCoordsCommand>
{
    public SetCadCoordsValidator()
    {
        When(x => x.CamCoordinates != null, () =>
        {
            RuleFor(x => x.CamCoordinates!.X)
                .NotNull().WithMessage(RequiredError)
                .ExclusiveBetween(CoordMin, CoordMax).WithMessage(RangeError);

            RuleFor(x => x.CamCoordinates!.Y)
                .NotNull().WithMessage(RequiredError)
                .ExclusiveBetween(CoordMin, CoordMax).WithMessage(RangeError);

            RuleFor(x => x.CamCoordinates!.Z)
                .NotNull().WithMessage(RequiredError)
                .ExclusiveBetween(CoordMin, CoordMax).WithMessage(RangeError);
        });

        When(x => x.PanCoordinates != null, () =>
        {
            RuleFor(x => x.PanCoordinates!.X)
                .NotNull().WithMessage(RequiredError)
                .ExclusiveBetween(CoordMin, CoordMax).WithMessage(RangeError);

            RuleFor(x => x.PanCoordinates!.Y)
                .NotNull().WithMessage(RequiredError)
                .ExclusiveBetween(CoordMin, CoordMax).WithMessage(RangeError);

            RuleFor(x => x.PanCoordinates!.Z)
                .NotNull().WithMessage(RequiredError)
                .ExclusiveBetween(CoordMin, CoordMax).WithMessage(RangeError);
        });
    }
}
