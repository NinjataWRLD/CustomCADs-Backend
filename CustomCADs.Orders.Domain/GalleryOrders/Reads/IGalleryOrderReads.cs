using CustomCADs.Orders.Domain.GalleryOrders.Entities;
using CustomCADs.Shared.Core.Domain.ValueObjects.Ids.Account;

namespace CustomCADs.Orders.Domain.GalleryOrders.Reads;

public interface IGalleryOrderReads
{
    Task<GalleryOrderResult> AllAsync(GalleryOrderQuery query, bool track = true, CancellationToken ct = default);
    Task<GalleryOrder?> SingleByIdAsync(GalleryOrderId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(GalleryOrderId id, CancellationToken ct = default);
    Task<int> CountAsync(UserId buyerId, CancellationToken ct = default);
}
