using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Orders.Application.Orders.Commands.PurchaseWithDelivery;

using static Constants.FluentMessages;

public class PurchaseOrderWithDeliveryValidator : Validator<PurchaseOrderWithDeliveryCommand, string>
{
    public PurchaseOrderWithDeliveryValidator()
    {
        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage(RequiredError);

        RuleFor(x => x.ShipmentService)
            .NotEmpty().WithMessage(RequiredError);
    }
}
