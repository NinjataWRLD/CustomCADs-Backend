using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Delivery.Endpoints.Shipments.Endpoints.Get.Shipment;

public record GetShipmentsRequest(
	ShipmentSortingType SortingType = ShipmentSortingType.RequestedAt,
	SortingDirection SortingDirection = SortingDirection.Descending,
	int Page = 1,
	int Limit = 20
);
