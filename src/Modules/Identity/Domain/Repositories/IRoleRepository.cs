using CustomCADs.Identity.Domain.Entities;

namespace CustomCADs.Identity.Domain.Repositories;

public interface IRoleRepository
{
    Task<AppRole?> GetByNameAsync(string name);
    Task<AppRole> AddAsync(AppRole role);
    void Remove(AppRole role);
    Task SaveChangesAsync();
}