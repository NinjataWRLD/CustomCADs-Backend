using CustomCADs.Orders.Application.Orders.Commands.Create;
using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Orders.Application.Orders.Commands.CreateWithDelivery;

using static Constants.FluentMessages;
using static OrderConstants;

public class CreateOrderWithDeliveryCommandValidator : Validator<CreateOrderCommand, OrderId>
{
    public CreateOrderWithDeliveryCommandValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
    }
}
