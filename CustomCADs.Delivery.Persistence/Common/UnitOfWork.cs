using CustomCADs.Delivery.Domain.Common;

namespace CustomCADs.Delivery.Persistence.Common;

public class UnitOfWork(DeliveryContext context) : IUnitOfWork
{
    public async Task SaveChangesAsync(CancellationToken ct = default)
        => await context.SaveChangesAsync(ct).ConfigureAwait(false);
}
