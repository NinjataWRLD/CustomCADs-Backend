using CustomCADs.Shared.Core.Common.TypedIds.Shipments;

namespace CustomCADs.Delivery.Domain.Shipments.Reads;

public interface IShipmentReads
{
    Task<ShipmentResult> AllAsync(ShipmentQuery query, bool track = true, CancellationToken ct = default);
    Task<Shipment?> SingleByIdAsync(ShipmentId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(ShipmentId id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
