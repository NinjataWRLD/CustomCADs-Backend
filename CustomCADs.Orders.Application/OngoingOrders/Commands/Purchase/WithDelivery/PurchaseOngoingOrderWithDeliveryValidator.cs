using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.WithDelivery;

using static Constants;
using static Constants.FluentMessages;

public class PurchaseOngoingOrderWithDeliveryValidator : Validator<PurchaseOngoingOrderWithDeliveryCommand, string>
{
    public PurchaseOngoingOrderWithDeliveryValidator()
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
                    .NotEmpty().WithMessage(RequiredError)
                    .Matches(Regexes.Email).WithMessage(EmailError);

                x.RuleFor(x => x.Phone)
                    .NotEmpty().WithMessage(RequiredError)
                    .Matches(Regexes.Phone).WithMessage(PhoneError);
            });
    }
}
