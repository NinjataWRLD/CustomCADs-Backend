using CustomCADs.Orders.Domain.Orders;
using CustomCADs.Shared.Application.Requests.Validator;
using CustomCADs.Shared.Core;
using FluentValidation;

namespace CustomCADs.Orders.Application.Orders.Commands.Create;

using static Constants.FluentMessages;
using static OrderConstants;

public class CreateOrderCommandValidator : Validator<CreateOrderCommand, OrderId>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
    }
}
