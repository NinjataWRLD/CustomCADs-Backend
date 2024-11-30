namespace CustomCADs.Accounts.Domain.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
}
