using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Purchase.WithDelivery;

using static Constants;
using static Constants.FluentMessages;

public class PurchaseCustomWithDeliveryValidator : CommandValidator<PurchaseCustomWithDeliveryCommand, string>
{
    public PurchaseCustomWithDeliveryValidator()
    {
        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.ShipmentService)
            .NotEmpty().WithMessage(RequiredError);

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
                    .Matches(Regexes.Email).WithMessage(EmailError);

                x.RuleFor(x => x.Phone)
                    .Matches(Regexes.Phone).WithMessage(PhoneError);
            });
    }
}
