using CustomCADs.Shared.Core.Common.TypedIds.Accounts;

namespace CustomCADs.Delivery.Application.Shipments.Queries.GetWaybill;

public record GetShipmentWaybillQuery(
    string ShipmentId,
    AccountId DesignerId
) : IQuery<byte[]>;
