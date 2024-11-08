namespace CustomCADs.Orders.Domain.Shared;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
}
