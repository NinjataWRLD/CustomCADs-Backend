using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation;

namespace CustomCADs.Carts.Application.ActiveCarts.Queries.Internal.CalculateShipment;

using static Constants.FluentMessages;

public class CalculateActiveCartShipmentValidator : QueryValidator<CalculateActiveCartShipmentQuery, CalculateShipmentDto[]>
{
    public CalculateActiveCartShipmentValidator()
    {
        RuleFor(x => x.Address).ChildRules(x =>
        {
            x.RuleFor(x => x.Country)
                .NotEmpty().WithMessage(RequiredError);

            x.RuleFor(x => x.City)
                .NotEmpty().WithMessage(RequiredError);
        });
    }
}
