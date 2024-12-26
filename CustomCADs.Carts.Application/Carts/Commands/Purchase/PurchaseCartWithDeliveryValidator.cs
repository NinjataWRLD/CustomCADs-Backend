using CustomCADs.Shared.Application.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Carts.Application.Carts.Commands.Purchase;

using static Constants.FluentMessages;

public class PurchaseCartValidator : Validator<PurchaseCartCommand, string>
{
    public PurchaseCartValidator()
    {
        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage(RequiredError);
    }
}
