using CustomCADs.Orders.Domain.Orders;
using FluentValidation;

namespace CustomCADs.Orders.Endpoints.Client.Put;

using static Constants.FluentMessages;
using static OrderConstants;

public class PutOrderRequestValidator : Validator<PutOrderRequest>
{
    public PutOrderRequestValidator()
    {
        RuleFor(o => o.Name)
            .NotEmpty().WithMessage(RequiredError)
            .Length(NameMinLength, NameMaxLength).WithMessage(LengthError);

        RuleFor(o => o.Description)
            .NotEmpty().WithMessage(RequiredError)
            .Length(DescriptionMinLength, DescriptionMaxLength).WithMessage(LengthError);
    }
}
