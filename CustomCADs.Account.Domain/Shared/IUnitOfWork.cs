namespace CustomCADs.Account.Domain.Shared;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
}
