using CustomCADs.Shared.Abstractions.Requests.Validator;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Shipments.Queries;
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
