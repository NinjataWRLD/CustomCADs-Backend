using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Purchase.Normal;

using static Constants.FluentMessages;

public class PurchaseActiveCartValidator : Validator<PurchaseActiveCartCommand, string>
{
    public PurchaseActiveCartValidator()
    {
        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage(RequiredError);
    }
}
