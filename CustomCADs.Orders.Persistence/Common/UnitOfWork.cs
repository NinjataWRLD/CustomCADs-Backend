using CustomCADs.Orders.Domain.Common;

namespace CustomCADs.Orders.Persistence.Common;

public class UnitOfWork(OrdersContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct).ConfigureAwait(false);
}
