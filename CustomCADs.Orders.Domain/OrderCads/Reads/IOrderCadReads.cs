namespace CustomCADs.Orders.Domain.OrderCads.Reads;

public interface IOrderCadReads
{
    Task<IEnumerable<OrderCad>> AllAsync(bool track = true, CancellationToken ct = default);
    Task<OrderCad> SingleByIdAsync(Guid id, bool track = true, CancellationToken ct = default);
    Task<OrderCad> ExistsByIdAsync(Guid id, CancellationToken ct = default);
    Task<OrderCad> CountByStatusAsync(Guid buyerId, CancellationToken ct = default);
}
