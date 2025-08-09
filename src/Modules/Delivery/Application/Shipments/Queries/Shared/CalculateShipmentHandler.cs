using CustomCADs.Delivery.Application.Contracts;
using CustomCADs.Shared.Abstractions.Delivery.Dtos;
using CustomCADs.Shared.Core.Common.Dtos;
using CustomCADs.Shared.UseCases.Shipments.Queries;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Shared;

public class CalculateShipmentHandler(IDeliveryService delivery)
	: IQueryHandler<CalculateShipmentQuery, CalculateShipmentDto[]>
{
	public async Task<CalculateShipmentDto[]> Handle(CalculateShipmentQuery req, CancellationToken ct)
	{
		CalculationDto[] calculations = await delivery.CalculateAsync(new(
			Weights: req.Weights,
			Country: req.Address.Country,
			City: req.Address.City,
			Street: req.Address.Street
		), ct).ConfigureAwait(false);

		return [.. calculations.Select(x => x.ToDto())];
	}
}
