﻿using CustomCADs.Shared.Core.Common;
using CustomCADs.Shared.Core.Common.TypedIds.Delivery;

namespace CustomCADs.Delivery.Domain.Shipments.Reads;

public interface IShipmentReads
{
    Task<Result<Shipment>> AllAsync(ShipmentQuery query, bool track = true, CancellationToken ct = default);
    Task<Shipment?> SingleByIdAsync(ShipmentId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(ShipmentId id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
