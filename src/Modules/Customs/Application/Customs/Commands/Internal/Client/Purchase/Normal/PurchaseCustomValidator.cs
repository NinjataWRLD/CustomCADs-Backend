﻿using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Customs.Application.Customs.Commands.Internal.Client.Purchase.Normal;

using static Constants.FluentMessages;

public class PurchaseCustomValidator : CommandValidator<PurchaseCustomCommand, string>
{
    public PurchaseCustomValidator()
    {
        RuleFor(x => x.PaymentMethodId)
            .NotEmpty().WithMessage(RequiredError);
    }
}
