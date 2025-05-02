using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Internal.Purchase.Normal;

using static Constants.FluentMessages;

public class PurchaseActiveCartValidator : CommandValidator<PurchaseActiveCartCommand, PaymentDto>
{
    public PurchaseActiveCartValidator()
    {
        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage(RequiredError);
    }
}
