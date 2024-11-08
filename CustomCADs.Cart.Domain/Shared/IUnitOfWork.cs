namespace CustomCADs.Cart.Domain.Shared;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
}
