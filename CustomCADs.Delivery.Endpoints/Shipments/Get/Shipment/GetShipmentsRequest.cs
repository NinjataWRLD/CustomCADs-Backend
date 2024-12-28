using CustomCADs.Delivery.Domain.Shipments.Enums;
using CustomCADs.Shared.Core.Common.Enums;

namespace CustomCADs.Delivery.Endpoints.Shipments.Get.Shipment;

public record GetShipmentsRequest(
    ShipmentSortingType SortingType = ShipmentSortingType.CreationDate,
    SortingDirection SortingDirection = SortingDirection.Descending,
    int Page = 1,
    int Limit = 20
);
