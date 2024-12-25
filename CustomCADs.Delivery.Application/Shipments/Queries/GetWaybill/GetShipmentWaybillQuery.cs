using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Queries.GetWaybill;

public record GetShipmentWaybillQuery(
    ShipmentId Id,
    AccountId DesignerId
) : IQuery<byte[]>;
