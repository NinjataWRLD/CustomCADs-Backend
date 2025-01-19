using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Purchase.Normal;

using static Constants.FluentMessages;

public class PurchaseOngoingOrderValidator : CommandValidator<PurchaseOngoingOrderCommand, string>
{
    public PurchaseOngoingOrderValidator()
    {
        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage(RequiredError);
    }
}
