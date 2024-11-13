using CustomCADs.Orders.Domain.GalleryOrders.Entities;

namespace CustomCADs.Orders.Domain.GalleryOrders.Reads;

public interface IGalleryOrderReads
{
    Task<IEnumerable<GalleryOrder>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<GalleryOrder?> SingleByIdAsync(GalleryOrderId id, bool track = true, CancellationToken ct = default);
    Task<bool> ExistsByIdAsync(GalleryOrderId id, CancellationToken ct = default);
    Task<int> CountAsync(CancellationToken ct = default);
}
