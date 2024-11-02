namespace CustomCADs.Catalog.Domain.Shared;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
}
