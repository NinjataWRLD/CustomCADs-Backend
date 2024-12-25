using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using CustomCADs.Shared.UseCases.Shipments.Commands;
using FluentValidation;

namespace CustomCADs.Delivery.Application.Shipments.SharedCommands;

using static Constants.FluentMessages;
using static ShipmentConstants;

public class CreateShipmentValidator : Validator<CreateShipmentCommand, (ShipmentId, decimal)>
{
    public CreateShipmentValidator()
    {
        RuleFor(x => x.Service)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.Info)
            .ChildRules(x =>
            {
                x.RuleFor(x => x.Count)
                    .InclusiveBetween(MinCount, MaxCount).WithMessage(RangeError);

                x.RuleFor(x => x.Weight)
                    .ExclusiveBetween(MinWeight, MaxWeight).WithMessage(RangeError);

                x.RuleFor(x => x.Recipient)
                    .NotEmpty().WithMessage(RequiredError);
            });

        RuleFor(x => x.Address)
            .ChildRules(x =>
            {
                x.RuleFor(x => x.Country)
                    .NotEmpty().WithMessage(RequiredError);

                x.RuleFor(x => x.City)
                    .NotEmpty().WithMessage(RequiredError);
            });

        RuleFor(x => x.Contact)
            .ChildRules(x =>
            {
                x.RuleFor(x => x.Email)
                    .NotEmpty().WithMessage(RequiredError)
                    .EmailAddress().WithMessage(EmailError);

                x.RuleFor(x => x.Phone)
                    .Matches(PhoneRegex).WithMessage(PhoneError);
            });
    }
}
