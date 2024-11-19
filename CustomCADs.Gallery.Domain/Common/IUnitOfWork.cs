namespace CustomCADs.Gallery.Domain.Common;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken ct = default);
}
