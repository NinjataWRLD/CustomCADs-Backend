using CustomCADs.Shared.Application.Dtos.Delivery;
using CustomCADs.Shared.Domain.TypedIds.Printing;

namespace CustomCADs.Customs.Application.Customs.Queries.Internal.Customers.CalculateShipment;

public record CalculateCustomShipmentQuery(
	CustomId Id,
	int Count,
	AddressDto Address,
	CustomizationId CustomizationId
) : IQuery<CalculateShipmentDto[]>;
