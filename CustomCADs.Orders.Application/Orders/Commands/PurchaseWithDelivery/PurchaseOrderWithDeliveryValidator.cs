using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Orders.Application.Orders.Commands.PurchaseWithDelivery;

using static Constants;
using static Constants.FluentMessages;

public class PurchaseOrderWithDeliveryValidator : Validator<PurchaseOrderWithDeliveryCommand, string>
{
    public PurchaseOrderWithDeliveryValidator()
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
