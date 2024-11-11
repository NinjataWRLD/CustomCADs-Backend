namespace CustomCADs.Orders.Domain.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
}
