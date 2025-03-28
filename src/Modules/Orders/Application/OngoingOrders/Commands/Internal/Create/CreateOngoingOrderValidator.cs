﻿using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Orders.Application.OngoingOrders.Commands.Internal.Create;

using static Constants.FluentMessages;
using static OngoingOrderConstants;

public class CreateOngoingOrderValidator : CommandValidator<CreateOngoingOrderCommand, OngoingOrderId>
{
    public CreateOngoingOrderValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
    }
}
