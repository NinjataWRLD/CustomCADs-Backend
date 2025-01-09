using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.Normal;

using static Constants.FluentMessages;

public class PurchaseOngoingOrderValidator : Validator<PurchaseOngoingOrderCommand, string>
{
    public PurchaseOngoingOrderValidator()
    {
        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage(RequiredError);
    }
}
