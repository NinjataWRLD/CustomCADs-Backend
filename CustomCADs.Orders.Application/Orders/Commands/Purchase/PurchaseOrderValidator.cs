using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Orders.Application.Orders.Commands.Purchase;

using static Constants.FluentMessages;

public class PurchaseOrderValidator : Validator<PurchaseOrderCommand, string>
{
    public PurchaseOrderValidator()
    {
        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage(RequiredError);
    }
}
