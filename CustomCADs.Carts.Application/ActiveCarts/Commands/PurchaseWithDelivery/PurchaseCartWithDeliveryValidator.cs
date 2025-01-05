using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.PurchaseWithDelivery;

using static Constants;
using static Constants.FluentMessages;

public class PurchaseCartWithDeliveryValidator : Validator<PurchaseActiveCartWithDeliveryCommand, string>
{
    public PurchaseCartWithDeliveryValidator()
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
