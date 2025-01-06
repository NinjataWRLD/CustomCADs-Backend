using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Carts.Application.ActiveCarts.Commands.Purchase;

using static Constants.FluentMessages;

public class PurchaseCartValidator : Validator<PurchaseActiveCartCommand, string>
{
    public PurchaseCartValidator()
    {
        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage(RequiredError);
    }
}
