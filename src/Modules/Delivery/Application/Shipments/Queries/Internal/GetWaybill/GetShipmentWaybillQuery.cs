using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Queries.Internal.GetWaybill;

public record GetShipmentWaybillQuery(
    ShipmentId Id,
    AccountId DesignerId
) : IQuery<byte[]>;
