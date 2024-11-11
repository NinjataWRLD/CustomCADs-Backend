namespace CustomCADs.Account.Domain.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
}
