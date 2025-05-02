using CustomCADs.Shared.Abstractions.Payment;
using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Customers.Purchase.Normal;

using static Constants.FluentMessages;

public class PurchaseCustomValidator : CommandValidator<PurchaseCustomCommand, PaymentDto>
{
    public PurchaseCustomValidator()
    {
        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage(RequiredError);
    }
}
