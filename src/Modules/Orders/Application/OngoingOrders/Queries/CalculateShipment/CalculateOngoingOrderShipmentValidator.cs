using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation;

namespace CustomCADs.Orders.Application.OngoingOrders.Queries.CalculateShipment;

using static Constants.FluentMessages;

public class CalculateOngoingOrderShipmentValidator : QueryValidator<CalculateOngoingOrderShipmentQuery, CalculateShipmentDto[]>
{
    public CalculateOngoingOrderShipmentValidator()
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
