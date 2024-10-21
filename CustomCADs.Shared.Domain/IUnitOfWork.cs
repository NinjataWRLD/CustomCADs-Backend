namespace CustomCADs.Shared.Domain;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
