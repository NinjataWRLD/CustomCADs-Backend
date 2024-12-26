using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Delivery.Application.Shipments.Commands.Edit;

using static Constants.FluentMessages;

public class EditShipmentValidator : Validator<EditShipmentCommand>
{
    public EditShipmentValidator()
    {
        RuleFor(x => x.Address)
            .ChildRules(x =>
            {
                x.RuleFor(x => x.Country)
                    .NotEmpty().WithMessage(RequiredError);

                x.RuleFor(x => x.City)
                    .NotEmpty().WithMessage(RequiredError);
            });
    }
}
