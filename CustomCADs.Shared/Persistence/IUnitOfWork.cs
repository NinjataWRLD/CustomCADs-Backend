namespace CustomCADs.Shared.Persistence;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
