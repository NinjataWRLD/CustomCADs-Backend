using CustomCADs.Shared.Application.Dtos.Delivery;

namespace CustomCADs.Shared.Application.UseCases.Shipments.Queries;

public record CalculateShipmentQuery(
	double[] Weights,
	AddressDto Address
) : IQuery<CalculateShipmentDto[]>;
