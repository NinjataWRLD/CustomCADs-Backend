using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.Core.Common.Dtos;
using FluentValidation;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment;

using static Constants.FluentMessages;

public class CalculateCustomShipmentValidator : QueryValidator<CalculateCustomShipmentQuery, CalculateShipmentDto[]>
{
	public CalculateCustomShipmentValidator()
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
