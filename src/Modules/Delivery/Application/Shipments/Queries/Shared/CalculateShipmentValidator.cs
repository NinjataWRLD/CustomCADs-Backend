using CustomCADs.Shared.Application.Abstractions.Requests.Validator;
using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Application.UseCases.Shipments.Queries;
using FluentValidation;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Shared;

using static Constants.FluentMessages;

public class CalculateShipmentValidator : QueryValidator<CalculateShipmentQuery, CalculateShipmentDto[]>
{
	public CalculateShipmentValidator()
	{
		RuleFor(x => x.Address).ChildRules(x =>
		{
			x.RuleFor(x => x.Country)
				.NotEmpty().WithMessage(RequiredError);

			x.RuleFor(x => x.City)
				.NotEmpty().WithMessage(RequiredError);

			x.RuleFor(x => x.Street)
				.NotEmpty().WithMessage(RequiredError);
		});
	}
}
