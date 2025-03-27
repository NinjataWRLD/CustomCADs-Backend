using CustomCADs.Delivery.Domain.Shipments;
using CustomCADs.Shared.Core.Common;

namespace CustomCADs.Delivery.Domain.Repositories.Reads;

public interface IShipmentReads
{
    Task<Result<Shipment>> AllAsync(ShipmentQuery query, bool track = true, CancellationToken ct = default);
    Task<Shipment?> SingleByIdAsync(ShipmentId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(ShipmentId id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
