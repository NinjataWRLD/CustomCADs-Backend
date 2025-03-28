﻿using CustomCADs.Shared.Abstractions.Requests.Validator;
using FluentValidation;

namespace CustomCADs.Orders.Application.CompletedOrders.Commands.Internal.Create;

using static CompletedOrderConstants;
using static Constants.FluentMessages;

public class CreateCompletedOrderValidator : CommandValidator<CreateCompletedOrderCommand, CompletedOrderId>
{
    public CreateCompletedOrderValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);

        RuleFor(x => x.Price)
            .ExclusiveBetween(PriceMin, PriceMax).WithMessage(RangeError);
    }
}
